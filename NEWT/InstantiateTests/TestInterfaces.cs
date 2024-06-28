namespace NEWT.InstantiateTests
{
    internal interface ITestClass
    {
        public ICollection<object> Dependencies { get; }
    }

    internal interface ITestClassWithArgs : ITestClass
    {
        public ICollection<object> Arguments { get; }
    }

    internal interface INoDependencyClass : ITestClass { }

    internal interface IOneDependencyClass : ITestClass { }

    internal interface ITwoDependencyClass : ITestClass { }

    internal interface ITwoDependencyWithDependenciesClass : ITestClass { }

    internal interface INoDependencyWithArgsClass : ITestClass { }

    internal interface IOneDependencyWithArgsClass : ITestClass { }

    internal interface IOneDependencyWithDependenciesAndArgsClass : ITestClass { }
}
