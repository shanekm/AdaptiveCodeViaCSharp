Adaptive Code via C#

1. Scrum

Scum vs Waterfall - ability to change stories/work
Product owner - provides link between client or customer and rest of development team.  Must be able to clearly communicate the vision of the product
Scrum master - Shields team from distractions.owns the process

Defect - Whener acceptance criteria are not met.
	A. acopaliptic
	B. behavioral defects
	C. cosmetic

Story points - Add business value
Board - Everyone is there to see it. May be able to make suggestions, ask questions


2. Dependencies
	Dependency - relationship between two distinct entities where one can not perform without the other
	1. First party - in the same code base (project/solution)
	2. Third party - external assembly
	3. Ubiquitous - .net framework

	- To keep code adaptive to change you must manage dependencies effectively.
	- assemblies are loaded at run time by CLR. So, even though you add reference, if that assembly is not used it will not generate error/or be loaded
	- using keywork - in a class, gets omitted if that assembly is NOT used, only for sugar coding code/easier to read


3. Managing Dependencies

	Code smell - code that could be potentially problematic. May becore technical dept that will need to be repaired
	Patterns - elegant solutions to complex problems, Interfaces that have been identified and codefied as patterns
	Anti-Patterns - began as patterns before slowly falling out of favor due to perceived negative side effects

	Entourage Anti-pattern - bringing in more dependencies than you think
		Ex. AccountController new(ing) repositories and services (sercurityService)
		Controllers: AccountController => 
			depends on => Services: ISecurityService + SecurityService => 
				depends on => Domain: IUserRepository (getting user) + UserRepository implementation =>
					depends on => NHibrernate

	Solution to Entourage anti-pattern: Stairway Pattern - split Interfaces and it's contrete implementation in seperate assemblies
		- Interfaces should NOT have external dependencies
		- by putting interfaces and their implementations in seperate assemblies, you can vary the two independently and clients only
			need to make a single reference - to the Interface

		AccountController => ISecurityService 
			=> (another assembly) SecurityService implementations 
				=> (another assembly/domain) IUserRepository =>
					=> (another assembly) UserRepository implementations
						=> NHibrenate

	NuGET - creating own packages
	Chocolatey - package management tool, like NuGet but its packages are applications and tools, not assemblies


4. Layering

	Layers - logical organization
	Layers vs. Tiers - logical seperation vs. physical deployment (ie. 4 layers but phisically on 2 tiers (on db server + ui/business logic server))
	
	1. Two layers - two logical groups of assemblies. [UI + Data access Interfaces] AND [Data access Implementations]
	2. Three layers - [UI] + [Business logic] + [Data access]
		Business logic - model the business domain to capture business processes, rules, and workflow
		* DDD - Use mapping to map objects between Domain (business logic) and ORM (ex. Entity framework) - this way Data Access no knowledge of business logic
			- Domain model assemblies have no dependency on Data access ORM
			- ORM could be easily replaced since Domain classes are NOT the same as ORM classes but only mapped
			- UI => Domain model => Object Relational Mapping => Data Access (ORM)

5. CQRM - Command query responsibility segregation - Asymetric layering
	- Path for data access (saving and reading) not always the same. Reading should be quick, while saving could have other bussiness logic, events etc.
	- Domain model handles commands, while quieries are directly hitting Data Access layer
	UI => Command => Logic => Data Access
	UI => Query => Data Access -> return data

	1. Commands - change state, do NOT return value
	2. Queries - requests for data, return data


5. Cross Cutting Concerns - Aspects / Attributes
	- shared functionality: logging, auditing, security - where does it go?
	Solution: AOP - Aspect oriented programming using Attributes [Log] or [Transactional] etc. - much cleaner


6. Interfaces and Design Patterns
	- Polymorphism - client code can interact with an object as if it was one type when it is actually another type
		IVehicle (StartEngine, StopEngine, Steer)
			a. Car
			b. Motorcycle
			c. Speedboat
		
	- defines the behavior that a class HAS, but not HOW this behavior is implemented
	- interfaces require a class to provide the working code to fulfill the interface
	- explicit vs implicit implementation (explicit: you define interface name)

  Extending Interfaces:
        1. dynamic keyword
        2. Impromptu NuGet library
        3. Mixins Re-mix, Re-motion
        4. Extension methods

	1. Adapter pattern - allows you to provide an object instance to client that has a dependency on an interface that your instance does not implement
		Adapter - implements expected Interface. Accepts Adaptee class or Adaptee class is defined inside adapter. Then calls Adaptee class
	2. Strategy pattern

	Duck Typing - Swan is a duck? => void Walk(), void Swim(), void Quack() 

	* How to treat an object as IF it was implementing an interface withOUT actually implementing an interface

	1. Using Dynamic Language Runtime (dynamic keyword)
		- having an Interface IDuck that is NOT implemented by Swan class. But the class MUST contain the method signature you're trying to run
		dynamic - dynamic variable can have any type. Its type can change during runtime. Performance issues/slow
			- acts like a placeholder for a type not know Until Runtime
			- don't use it. However only use it when reflection is needed

		dynamic a = 1;
		Console.WriteLine(a);

		// Dynamic now has a different type.
		a = new string[0];

	2. Impromtu Interface - ActLike<T> - pass in class (Swan) and receive Interface (IDuck) instance
		- must download NuGet package

			var swan = new Swan();
			var swanAsDuck = Impromtu.ActLike<IDuck>(swan);
			if (swanAsDuck != null)
			{
				swanAsDuck.Walk();
				...
			}

	3. Mixins
		- NuGet: Re-motion and Re-mix
		- allow for a class to act as if implements methods/properties from several different interfaces even though it actually doesn't implement any

	4. Extension Methods - allow for extending Interfaces, adding new functionality to already existing types without needing to access the source type
		
		public static class MyExtensions {
			public static void FurtherInterfaceMethodA(this IMyInterface target, int extraParameter)
			{
				...


7. Unit Testing
	- process of writing code that tests other code
	- Act, Arrange, Assert
	- MOQ - framework for setting up mocks
		a. When set up an mocked object's properties are set to defaults (int = 0, bool = false, string = null etc)
		b. Mock .Object is an interface
		c. Set up .Object how it should behave when a method is called
		d. Prefix all mocked objects with 'mock' keyword for consistency

	[TestInitilize] - ran before Each test

8. Refactoring
	- process of incrementally improving the design of existing code
	AbstractBase class AccountService 
		a. SilverAccount
		b. GoldAccount
		c. PlatinumAccoutn - each having CalculateRewardPoints() method

	// Refactoring - implemented by Silver, Gold, Platinum, Standard accounts have no reward points
	// Interface can be implemented by classes that have RewardPoints
	interface IRewardCard
	{
		int RewardPoints { get; }
	}
	void CalculateRewardPoints(decimal amount, decimal accountBalance); 


PART II - SOLID PRINCIPLES

1. Single Responsibility Principle (SRP)
	- write code that has one and only one reason to change. If a class has more than one reason to change, it has more
		than one responsibility
	- classes with more than single responsibility should be broken up
	"Big ball of mud" - class or group of classes that is mixed up with different responsabilities
	- refactor for Adoptability and NOT for readability

	TradeProcessor() class
		- reads stream, parsing, validating, logging - all in one class

		After Refactoring:
			a. Stream can be easily replaced by other service (ie. web service) => new ITradeDataProvider (feeds service)
			b. Validation rules can change => ITradeValidator, any implementation can be chanced, added easily
			c. Logging erros => ILogger
			d. Db implementation / storage can be easily changed - ITradeStorage

	Extending SRP:
		Decorator Patterns - ensures that each class has a single responsibility. Functionailty can be added to
			an existing class that implements certain Interface.
			- Attach additional responsibilities to an object dynamically

			ConcreteComponent.Something() : IComponent 
			DecoratorComponent.Something() : IComponent => Constructor(ConcreteComponent)
				Something( 
					// do other work
					// do concreteComponent work: Something();
				)
			
			1. Predicate Decorators:
				- Useful for hiding conditional execution of code from clients (ie. hiding DateTeste class functionality)
				- ex. DateTester is executed in predicateDecorator although hidden from Program/client. only Run() method visible in client

				public class DateTested {
					public bool PerformTest get { // some logic, calculation }

				// Decorator pattern as before
				public PredicateDecorator(IComponent component){
					public void Run()
					{
						DateTester dateTester = new DateTester()
						if (dateTester.PerformTest) // Client is not are of dateTester PredicateDecorator class has only Run() public method
						... 
					}
				}

			2. Branching Decorators
				- useful to execute something on the false or true branch IComponent, based on condition

				public class BranchedComponent : IComponent
				{
					public BranchedComponent(IComponent trueComponent, IComponent falseComponent, IPredicate){
						// set private fields
					}

					public void DoSomething(){
						if (true)
							trueComponent.Something(); // implementation of True
						else
							falseComponent.Something(); // implementation of False
					}
				}

			3. Lazy Decorators
				- allows clients to be provided with a reference to an interface that will not be instaniated until its first use
				- Lazy<T>

			4. Profiling Decorators
				- stopwatch = new StopWatch

				public class SlowComponent : IComponent{
					public SlowComponent {
						// init stopwatch
						stopwatch = new Stopwatch();
					}

					public DoSomething(){
						stopwatch.Start();
						...
						stopwatch.Stop();
						stopwatch.ElapsedMilliseconds / 1000 // seconds
					}
				}

			5. Asynchronous Decorators
				- run on different thread than the client
				- allows to send in synch component and send it further in async manner using decorator
				-  only fire and forget. Methods that have NO return value

		b. Composite
			- to alow you to treat many instances of an interface as if they were just one instance
			- add an RemoveComponent methods are NOT part of IComponent interface. Leaf class still adheres to SRP
			- more functionailty can be added to ALL implementation of IComponent without changing the implementing concrete types
				otherwise clients of IComponent would have to change.
			- compose objects into tree structures to represent part-whole hierarchies
			- composite lets clients treat individual objects and compositions of objects uniformly.

2. Open/Closed Principle - OCP
	- open for extension, closed for modification
	- should be open to extension by containing defined extension points where future functionality can hook into the existing code 
		and provide new behaviours

	Code without extension points:
		TradeProcessorClient => uses TradeProcessor - ProcessTrades(): TradeProcessorClient directly depends on TradeProcessor class
			- when new functionality is needed both classes will have to be modified

	Ways to allow for extension:
		a. Virtual methods
		b. Abstract methods
		c. Interface inheritance

	A. Virtual Methods
		TradeProcessorClient => uses TradeProcessor - ProcessTrades(): virtual => new version TradeProcessor2 - ProcessTrades: override
			- any class that marks one of its members as virtual is open to extension (via inheritance)
			- TradeProcessorClient does not need to change and you can supply client with new version of TradeProcessor(2)
			- Only Methods can be Virtual, class doesn't have to be abstract
		
	B. Abstract Methods
		- more flexible than virtual methods
		- client may only be aware of Base class and not implementation
		TradeProcessorClient => uses TradeProcessorAbstract => can use either implementation 
		- many versions of the TradeProcessor are provided both inherit ProcessTrades() with their own implementation
		- client ONLY depends on abstract Base class, so either concrete subclass or new subclass could be provided

	C. Interface Inheritance
		- best, most flexible
		- client's dependency on a class is replaced with delegation to an interface
		- many classes have concrete implementation of the interface
		TradeProcessorClient => uses ITradeProcessor => implemented by many ProcessTrades()

3. Liskov Substitution Principle (LSP)
	- collection of guidelines for creating inheritance hierarchies in which a client can reliably use any class 
		or subclass without compromising the expected behavior.
	- if LSP is NOT followed, the new subclass might force changes to client, base class or interface
	- as long as there are no changes to the interface there should be no reason to change any existing code

	A. Contracts (Guard Clauses) - program to contracts, conditions necessary for a method to run reliably and without fault
		- preconditions cannot be strengthened in a subtype - switch between supertype/subtype would be different (LSP would fail)
		- postconditions cannot be weakened in a subtype - switch between supertype/subtype would be different (LSP would fail)
		- invariants of the supertype must be preserved in a subtype - must be maintained otherwise (LSP would fail)
		
		a. Preconditions - check parameters coming IN to the class method

			public decimal CalculateShippingCost(float weight){
				if (weight <= 0) // Precondition
					throw new ArgumentOutOfRangeException("weight", "package weight must be positive non-zero")
			}
	
		b. Postconditions - check parameters coming OUT the class method, checks wheter an object is being left in a valid state on exit
			- placed at the end of method alfter all edits to state have been made

			public decimal CalculateShippingCost(float weight){
				if (weight <= 0)
					throw new ArgumentOutOfRangeException("weight", "package weight must be positive non-zero")

				...
				// Postcondition
				if (shippingCost <= decimal.Zero)
					throw new ArgumentOutOfRangeException("weight", "return value is out of range")

				return shippingCost
			}

		c. Data Invariants - predicate that remain true for the lifetime of an object
			- true after construction and must remain true until the object is out of scope

			public class ShippingStrategy{
				
				protected decimal rate; // protected - only settable in constructor
				// if it were public it would be settable outside of ShippingStrategy class - WRONG!

				publi ShippingStrategy(decimal rate){
					if (rate <= decimal.One)
						throw new ArgumentOutOfRangeException("rate", "Flate rate must be positive greater than 0")
				}
			}


	B. Code Contracts
		System.Diagnostics.Contracts - .net attribute classes that allow for pre/post/data invariant checks
		- set in Visual Studio => Code Contracts

			Contract.Requires<ArgumentOutOfRangeException>(weight > 0, "Package weight must be greater than 0"); // Precondition
			Contract.Ensures(Contract.Result<decimal>() > 0) // 
			Contract.Invariant(flatRate > 0m, "Flat rate must be positive and non-zero"); // Invariant

	C. Convariance and Contravariance - refers to Generic parameters
		variance - a term applied to the expected behaviour of subtypes in a class hierarchy containing complex types
		invariance - NOT able to be base/child flexible (IDictionary<Child, Parent> != IDictionary<Parent, Child>)

		a. Covariance - Return T (out)
			- accepts Supertype/Base, will also accept Subtype/Child without any casting (no Casting!) and Returns Both
			Supertype/Base (field1, field2, Method1()) => Subtype/Child (field1, field2, new field3, Method1(), new Method2())

		b. Contravariance - Accepts T (in) parameter
			- accepts Subtype/Child in place of Supertype/Base

4. Interface Segregation Principle (ISP) => see Interfaces Solution 
	IMPORTANT - only reason to split interfaces is to extend it/ or decorate it (using docorator pattern)
	- no client consuming an interface should be forced to depend on methods it does not use
	- later when you for each you will have to check if (entity is ISavable) it will throw an error as you're expecting all concrete types
		 to have implementation of all members of interface
	- if new property or method is added you need to modify all concrete classes/implementations (some may not need all the functionality)

    public interface ISavable
    {
        void Save();
    }

	You need to add loging:
		Problems: 
			- mismatch because some users of this interface don't need loging, you would have to modify all implementations
			- will throw an error when enumerating ISavable in a list etc
			- functionality that some clients use, others don't should not be accessible (encapsulation)

	Better to add new interface

	public interface IExtendedSavable : ISavable
    {
        void Log(string message);
    }

	- Now classes that already implemented ISavable still work because no properties/methods were added, and only new class implements extension
	- Both implementations can be used for processing, example send in to a method (Base method is accepted)

	AnotherClass anotherClass = new AnotherClass();
	anotherClass.Save("Extended save!");
	anotherClass.Save(); // ISavable save called
	SendInInterface(anotherClass); 

	ISavable s1 = new SomeClass(); // s1 class has NO access to Log() method - good 
    SendInInterface(s1); // This also works

	public static void SendInInterface(ISavable savable)
    {
        if (savable is ISavable)
        {
            // do stuff
        }
    }

	Example 3
		Reading/Writing - two users use interfaces however reading class does NOT need to know about writing implementation

5. Dependency Injection
	Class should be Proxiable - alternative version/implementation (known as proxy) can be supplier to the client (ie. virtual methods etc)

	a. Method injection 
		- injected instance is used for the lifetime of the client
		var taskService = taskService.GetAllTasks(settings); // injecting via method
	b. Property injection
		- benefit is that instance can be changed at run time on the client
		var taskService.Service = settings;
	
	IoC Container - holds mappings between interfaces and concrete types. Types are mapped to interfaces
	Register => Resolve => Release pattern

	public interface IContainer : IDisposable
	{
		void Register<TInterface, TImplementation>() where TImplementation : TInterface;
		TImplementation Resolve<TInterface>();
		void Release();
	}

	A. Object Lifetime
		- how do you control object lifetime (ex. SqlConnection used in TaskService?)
		- if SqlConnection was injected into TaskService it would life for the lifetime of the application

		ex1: Implementing IDisposable
		- no concrete class implements ITaskService and IDisposable
		- When using Constructor Injection, class being passed in should not manually dispose of the dependency itself

		// Connection will be disposed when TaskService is disposed
		public class TaskService: ITaskService, IDisposable{
			public TaskService(IDbConnection connection){
				this.connection = connection;
			}

			... // rest of code
		}

		ex2. Connection Factory
		- replace manual object instantiation with delegation to class whose purpose is to create objects

		// Injected into TaskService so that you can retrieve connection without manually constructinig it
		// keeping the service testable through mocking
		public interface IConnectionFactory{
			// IDbConnection implements IDisposable
			IDbConnection CreateConnection();
		}

		public class TaskService: ITaskService, IDisposable{
			private readonly IConnectionFactory connectionFactory
			public TaskService(IConnectionFactory connectionFactory){
				this.connectionFactory = connectionFactory;
			}

			using (var connection = connectionFactory.CreateConnection()){ // connection will be disposed by using{} block end
				connection.Open;
				...
			}
		}

		ex3. Responsible Owner Pattern
		- in case class does not implement IDisposable
		try {
			 connection.Open();
			 ...
			 // read/while from db
		} finally {
			if (connection is IDisposable)
			{
				var disposableConnection = connection as IDisposable;
				disposableConnection.Dispose();
			}
		}

		return allTasks;

		ex4. Factory Isolation Pattern
		- only required when target interface does not implement IDisposable
		- only applicable when the product of the factory may or may not implement IDisposable. So it effectively forces all implementations to provide a public Dispose() method
			adding using{} block will dispose of IdbConnection object
		- factory isolation pattern replaces the common Create() method which returns an instance of a class, instead providing With() method
			which accepts lambda method that has the factory product as a parameter
		- allows to manage the lifetime of a database connection
		- advantage here is that the lifetime of the factory product is explicitly linked to the lambda method scope (lambda ends = instance of class ends as well)

		public class IsolationConnectionFactory : IConnectionIsolationFactory {
			public void With(Action<IDbConnection do){
				using (var connecion = CreateConnection()){
					do(connection);
				}
			}
		}

		public IEnumerable<TaskDto> GetAllTasks(){
			// Usage
			connectionFactory.With(connection => {
				connection.Open();
				// get data/while read etc
			})
		}

	B. Service Locator Anti-Pattern
		- incorrectly creates types in default() /empty constructor (bad!)
		- no DI, "don't call us, we'll call you" pattern broken
		- have to search the code what is being constructed instead of constructor injection easily readable

		public interface IServiceLocator : IServiceProvider{
			object GetInstance(Type serviceType);
			object GetInstance(Type serviceType, string key);
			...
		}

		// Usage
		var taskService = ServiceLocator.Current.GetInstance<ITaskService>();

	C. Composition Root
		- location in an application where DI should happen
		- where classes are constructed when using Poor Man's DI or interface mapping container
		- as close to entry point of the application as possible

		MVC example: (Global.asax)
		public class MvcApplication : HttpApplication{
			AreaRegistration.RegisterAllAreas();
			...
			Container container = new UnityContainer();
			Container.RegisterType<ISettings, Settings>(); // Mapping

		}

	D. Convention over Configuration
		- instead of using Container.RegisterType or XML use convention
		- weakly typed (if error or no match found only error thrown at run time, whereas strongly typed using class error during compilation)

		Container.RegisterTypes(
			AllClasses.FromAssembliesInBasePath(), // bin folder
			WithMappings.FromMachingInterface(), // ITaskService => TaskService (name mapping)
			WithName.Default()
		);


6. Sprints
	a. Factory Isolation Pattern
	b. Refactoring AdoNetRepository to not use Factory isolation pattern
	c. Moved mapper and Repo outside of controller and created service (reader/writer) that map contract classes (Room) to ViewModels controller uses
		- created reader/writer and concrete implementation
		- Created RepositoryRoomViewModelService service that implements reader and writer
        - IoC will pass in Concrete implementaion RepositoryRoomViewModelService (read / write permissions)

	d. Adding 'Decorator' for HTML markdown for IReader
	e. Using Service Locator pattern (Anti-Pattern) for ContentFilterAttribute (blocked words)
		- Class that returns a static, hardcoded list and provide a more data-driven implementaion => Service Locator
	f. ContentFilterAttribute for validating view model MessageViewModel