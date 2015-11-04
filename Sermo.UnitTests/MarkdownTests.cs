using System.Linq;
using Moq;
using NUnit.Framework;
using Sermo.Markdown;
using Sermo.UI.Contracts;

namespace Sermo.UnitTests
{
    [TestFixture]
    public class MarkdownTests
    {
        [SetUp]
        public void SetUp()
        {
            markdown = new MarkdownDeep.Markdown();
            message1 = new MessageViewModel { AuthorName = "Dianne", ID = 1, RoomID = 12345, Text = "Test!" };
            mockRoomViewModelReader = new Mock<IRoomViewModelReader>();
            var roomMessages = new[] { message1 };
            mockRoomViewModelReader.Setup(reader => reader.GetRoomMessages(It.IsAny<int>())).Returns(roomMessages);
        }

        private MessageViewModel message1;

        private Mock<IRoomViewModelReader> mockRoomViewModelReader;

        private MarkdownDeep.Markdown markdown;

        [Test]
        [TestCase("This message has only paragraph markdown...", "<p>This message has only paragraph markdown...</p>\n")
        ]
        [TestCase("This message has *some emphasized* markdown...", 
            "<p>This message has <em>some emphasized</em> markdown...</p>\n")]
        [TestCase("This message has **some strongly emphasized** markdown...", 
            "<p>This message has <strong>some strongly emphasized</strong> markdown...</p>\n")]
        public void MessageTextIsAsExpectedAfterMarkdownTransform(string markdownText, string expectedText)
        {
            message1.Text = markdownText;
            var markdownDecorator = new RoomViewModelReaderMarkdownDecorator(mockRoomViewModelReader.Object, markdown);

            var roomMessages = markdownDecorator.GetRoomMessages(12345);

            var actualMessage = roomMessages.FirstOrDefault();

            Assert.That(actualMessage, Is.Not.Null);

            Assert.That(actualMessage.Text, Is.EqualTo(expectedText));
        }
    }
}