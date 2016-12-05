using System;
using System.Configuration;
using System.IO;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace WindowsFormsApplication1
{
    public interface Icomputer
    {
        //void add(int A, int B);
        //void sub(int A, int B);

        void addsuper(int A, int B);

        void subsuper(int A, int B);
    }

    public class computerSuper : Icomputer
    {
        public void addsuper(int A, int B)
        {
            int C;
            C = A + B;
            Console.WriteLine("super" + C);
        }

        public void subsuper(int A, int B)
        {
            int C;
            C = A - B;
            Console.WriteLine("super" + C);
        }
    }

    public class Parent
    {
        private readonly ChildA _classA;
        private readonly ChildB _classB;

        public Parent()
        {
        }

        //指定依赖注入构造函数
        [InjectionConstructor]
        public Parent(ChildA chA, ChildB chB)
        {
            _classA = chA;
            _classB = chB;
        }

        public override string ToString()
        {
            // 年长的父母依赖与孩子。
            return string.Format("The elder depend on {0} and {1}.", _classA, _classB);
        }
    }

    /// <summary>
    ///     孩子类
    /// </summary>
    public class ChildA : Parent
    {
        public override string ToString()
        {
            return "ChildA";
        }
    }

    /// <summary>
    ///     孩子类
    /// </summary>
    public class ChildB : Parent
    {
        public override string ToString()
        {
            return "ChildB";
        }
    }

    public class computer
    {
        public void add(int A, int B)
        {
            int C;
            C = A + B;
            Console.WriteLine(C);
        }

        public void sub(int A, int B)
        {
            int C;
            C = A - B;
            Console.WriteLine(C);
        }
    }

    


    public static class Program
    {
        /// <summary>
        ///     应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            IUnityContainer container = new UnityContainer();
           
            ReadConfiguration("unity.config", container);

            var com = container.Resolve<Icomputer>();
            com.addsuper(4, 5);
            com.subsuper(5, 4);
        }

        private static void ReadConfiguration(string configName, IUnityContainer container)
        {
            var configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configName);
            var map = new ExeConfigurationFileMap {ExeConfigFilename = configFilePath};

            var configuration = ConfigurationManager.OpenMappedExeConfiguration
                (map, ConfigurationUserLevel.None);

            var unitySection = (UnityConfigurationSection) configuration.GetSection("unity");
            foreach (var containerElement in unitySection.Containers)
                unitySection.Configure(container, containerElement.Name);
        }
    }
}
