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
