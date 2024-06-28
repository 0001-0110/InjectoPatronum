using InjectoPatronum;

namespace NEWT.ExecuteTests
{
	[TestClass]
	public class ExecuteTests
	{
		private class Methods
		{
			public interface IUseless { }

			public class Useless : IUseless { }

			public int Value { get; private set; } = 0;

			public static int StaticAdd(IUseless useless, int a, int b)
			{
				if (useless == null)
					throw new ArgumentNullException();

				return a + b;
			}

			public static void StaticAppend(IUseless useless, List<object> list)
			{
				if (useless == null)
					throw new ArgumentNullException();

				list.Add(100);
			}

			public int Add(IUseless useless, int a, int b)
			{
				if (useless == null)
					throw new ArgumentNullException();

				return a + b;
			}

			public void Set(IUseless useless, int value)
			{
				if (useless == null)
					throw new ArgumentNullException();

				Value = value;
			}
		}

		private IDependencyInjector _injector;

		[TestInitialize]
		public void SetUp()
		{
			_injector = new DependencyInjector()
				.Map<Methods.IUseless, Methods.Useless>();
		}

		private void Truc(Delegate @delegate) { }

		[TestMethod]
		public void TestStaticVoid()
		{
			List<object> list = new List<object>();
			_injector.Execute(typeof(Methods), Methods.StaticAppend, [list]);
			Assert.IsTrue(list.Any());
		}

		[TestMethod]
		public void TestStaticAdd()
		{
			Assert.AreEqual(5, _injector.Execute<int>(typeof(Methods), Methods.StaticAdd, [1, 4]));
		}

		[TestMethod]
		public void TestAdd()
		{
			Methods methods = new Methods();
			Assert.AreEqual(8, _injector.Execute<int>(methods, methods.Add, [4, 4]));
		}

		[TestMethod]
		public void TestVoidSet()
		{
			var methods = new Methods();
			_injector.Execute(methods, methods.Set, [67]);
			Assert.AreEqual(67, methods.Value);
		}
	}
}
