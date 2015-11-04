using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics.Contracts;
using Sermo.Data.Contracts;

namespace Sermo.Data.AdoNet
{
    public interface IConnectionIsolationFactory
    {
        void With(Action<IDbConnection> action);    
    }

    public class IsolationConnectionFactory : IConnectionIsolationFactory
    {
        private readonly DbProviderFactory dbProviderFactory;

        // Will be disposed when Lambda exists
        public void With(Action<IDbConnection> action)
        {
            using (var connection = dbProviderFactory.CreateConnection())
            {
                connection.ConnectionString = "";
                connection.Open();
                action(connection);
            }
        }
    }

    // Take 1 - Factory Isolation Pattern
    // Only required when the target interface does not implement IDisposable
    // Allows to manage the lifetime of a database connection
    public class AdoNetRoomRepositoryTake1 //: IRoomRepository
    {
        private readonly IApplicationSettings applicationSettings;

        private readonly IConnectionIsolationFactory factory; // Using isolation factory

        public AdoNetRoomRepositoryTake1(IApplicationSettings applicationSettings, IConnectionIsolationFactory factory)
        {   
            this.applicationSettings = applicationSettings;
            this.factory = factory;
        }

        public void CreateRoom(string name)
        {
            // Using isolation factory
            factory.With(
                connection =>
                    {
                        using (var transaction = connection.BeginTransaction())
                        {
                            // perform read operations
                        }
                    });
        }
    }

    // Take 2
    public class AdoNetRoomRepositoryTake2 //: IRoomRepository
    {
        private readonly IApplicationSettings applicationSettings;

        private readonly DbProviderFactory factory;

        // DbProviderFactory will be instantiated with new() via IoC
        public AdoNetRoomRepositoryTake2(IApplicationSettings applicationSettings, DbProviderFactory factory)
        {  
            this.applicationSettings = applicationSettings;
            this.factory = factory;
        }

        public void CreateRoom(string name)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = applicationSettings.GetValue("SermoConnectionString");
                connection.Open();
                // read data etc

            }
        }
    }

    public class AdoNetMessageRepository : IMessageRepository
    {
        private readonly IApplicationSettings applicationSettings;

        private readonly DbProviderFactory databaseFactory;

        public AdoNetMessageRepository(IApplicationSettings applicationSettings, DbProviderFactory databaseFactory)
        {
            Contract.Requires<ArgumentNullException>(applicationSettings != null); // Preconditions
            Contract.Requires<ArgumentNullException>(databaseFactory != null);

            this.applicationSettings = applicationSettings;
            this.databaseFactory = databaseFactory;
        }

        public void AddMessageToRoom(int roomID, string authorName, string text)
        {
            using (var connection = databaseFactory.CreateConnection())
            {
                connection.ConnectionString = applicationSettings.GetValue("SermoConnectionString");
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "dbo.add_message_to_room";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = transaction;

                    var roomIdParameter = command.CreateParameter();
                    roomIdParameter.DbType = DbType.Int32;
                    roomIdParameter.ParameterName = "room_id";
                    roomIdParameter.Value = roomID;
                    command.Parameters.Add(roomIdParameter);

                    var authorNameParameter = command.CreateParameter();
                    authorNameParameter.DbType = DbType.String;
                    authorNameParameter.ParameterName = "author_name";
                    authorNameParameter.Value = authorName;
                    command.Parameters.Add(authorNameParameter);

                    var textParameter = command.CreateParameter();
                    textParameter.DbType = DbType.String;
                    textParameter.ParameterName = "text";
                    textParameter.Value = text;
                    command.Parameters.Add(textParameter);

                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<MessageRecord> GetMessagesForRoomID(int roomID)
        {
            var roomMessages = new List<MessageRecord>();

            using (var connection = databaseFactory.CreateConnection())
            {
                connection.ConnectionString = applicationSettings.GetValue("SermoConnectionString");
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "dbo.get_room_messages";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = transaction;

                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            var id = reader.GetString(reader.GetOrdinal("id"));
                            var authorName = reader.GetString(reader.GetOrdinal("author_name"));
                            var text = reader.GetString(reader.GetOrdinal("text"));
                            roomMessages.Add(new MessageRecord(roomID, authorName, text));
                        }
                    }
                }
            }

            return roomMessages;
        }
    }
}