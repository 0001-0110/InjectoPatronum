using InjectoPatronum;

namespace NEWT.InstantiateTests
{
    [TestClass]
    public class DependencyInjectorTest
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        IDependencyInjector injector;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [TestInitialize]
        public void SetUp()
        {
            injector = new DependencyInjector()
                .Map<INoDependencyClass, NoDependencyClass>()
                .Map<IOneDependencyClass, OneDependencyClass>()
                .Map<ITwoDependencyClass, TwoDependencyClass>()
                .Map<ITwoDependencyWithDependenciesClass, TwoDependencyWithDependenciesClass>()
                .Map<INoDependencyWithArgsClass, NoDependencyWithArgsClass>()
                .Map<IOneDependencyWithArgsClass, OneDependencyWithArgsClass>()
                .Map<IOneDependencyWithDependenciesAndArgsClass, OneDependencyWithDependenciesAndArgsClass>()
                .MapGeneric(typeof(IGenericInterface<>), typeof(GenericClass<>));
        }

        [TestMethod]
        [DataRow(typeof(INoDependencyClass), typeof(NoDependencyClass))]
        [DataRow(typeof(IOneDependencyClass), typeof(OneDependencyClass))]
        [DataRow(typeof(ITwoDependencyClass), typeof(TwoDependencyClass))]
        [DataRow(typeof(ITwoDependencyWithDependenciesClass), typeof(TwoDependencyWithDependenciesClass))]
        public void TestDependencyInjectionWithoutArguments(Type @interface, Type implementation)
        {
            object? result = injector.Instantiate(implementation, []);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, @interface);
            Assert.IsTrue(((ITestClass)result).Dependencies.All(dependency => dependency != null));
            foreach (object dependency in ((ITestClass)result).Dependencies)
                Assert.IsTrue(((ITestClass)dependency).Dependencies.All(dependency => dependency != null));
        }

        [TestMethod]
        [DataRow(typeof(INoDependencyWithArgsClass), typeof(NoDependencyWithArgsClass), "test")]
        [DataRow(typeof(INoDependencyWithArgsClass), typeof(NoDependencyWithArgsClass), 10)]
        [DataRow(typeof(INoDependencyWithArgsClass), typeof(NoDependencyWithArgsClass), 10f)]
        [DataRow(typeof(IOneDependencyWithArgsClass), typeof(OneDependencyWithArgsClass), "test")]
        [DataRow(typeof(IOneDependencyWithArgsClass), typeof(OneDependencyWithArgsClass), 90)]
        [DataRow(typeof(IOneDependencyWithArgsClass), typeof(OneDependencyWithArgsClass), true)]
        [DataRow(typeof(IOneDependencyWithDependenciesAndArgsClass), typeof(OneDependencyWithDependenciesAndArgsClass), "", 10)]
        [DataRow(typeof(IOneDependencyWithDependenciesAndArgsClass), typeof(OneDependencyWithDependenciesAndArgsClass), false, true)]
        [DataRow(typeof(IOneDependencyWithDependenciesAndArgsClass), typeof(OneDependencyWithDependenciesAndArgsClass), 10f)]
        public void TestDependencyIjectionWithArgumengts(Type @interface, Type implementation, params object[] arguments)
        {
            object? result = injector.Instantiate(implementation, arguments);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, @interface);
            Assert.IsTrue(((ITestClass)result).Dependencies.All(dependency => dependency != null));
            foreach (object dependency in ((ITestClass)result).Dependencies)
                Assert.IsTrue(((ITestClass)dependency).Dependencies.All(dependency => dependency != null));
            Assert.IsTrue(arguments.All(arg => ((ITestClassWithArgs)result).Arguments.Contains(arg)));
        }

        [TestMethod]
        [DataRow(typeof(IGenericInterface<int>), typeof(GenericClass<int>))]
        [DataRow(typeof(IGenericInterface<string>), typeof(GenericClass<string>))]
        public void TestGenericDefinitionDependency(Type @interface, Type implementation, params object[] arguments)
        {
            object? result = injector.Instantiate(implementation, arguments);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, @interface);
            Assert.IsTrue(((ITestClass)result).Dependencies.All(dependency => dependency != null));
            foreach (object dependency in ((ITestClass)result).Dependencies)
                Assert.IsTrue(((ITestClass)dependency).Dependencies.All(dependency => dependency != null));
            Assert.IsTrue(arguments.All(arg => ((ITestClassWithArgs)result).Arguments.Contains(arg)));
        }
    }
}
