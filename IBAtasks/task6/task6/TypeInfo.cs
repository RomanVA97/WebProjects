using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace task6
{
    class CustomTypeInfo
    {

        public static void DisplayInformation(Object obj)
        {
            TypeInfo typeInfo = obj.GetType().GetTypeInfo(); //boy.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            Console.WriteLine("Список полей");
            foreach (FieldInfo info in typeInfo.GetRuntimeFields())
            {
                Console.WriteLine(info);
            }
            Console.WriteLine("Список методов");
            foreach (MethodInfo info in typeInfo.GetRuntimeMethods())
            {
                Console.WriteLine(info);
            }
            Console.WriteLine("Список свойств");
            foreach (PropertyInfo info in typeInfo.GetRuntimeProperties())
            {
                Console.WriteLine(info);
            }


            Console.WriteLine("Список конструкторов");
            foreach (ConstructorInfo info in typeInfo.DeclaredConstructors)
            {
                Console.WriteLine(info);
            }


        }

    }
}
