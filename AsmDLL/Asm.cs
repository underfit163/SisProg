using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AsmDLL
{
    /// <summary>
    /// Класс для вычисления исключаещего или
    /// </summary>
    public class AsmClass
    {
        static void Main(string[] args)
        {
                    
            string assemblyName = "Asm";
            string modName = "Asm.dll";
            string typeName = "AsmDLL";


            //Создаем домен приложения, в котором будем создавать наш класс
            AppDomain domain = System.Threading.Thread.GetDomain();
            //Создаем имя сборки
            AssemblyName name = new AssemblyName(assemblyName);
            //Для создания сборки
            AssemblyBuilder builder = domain.DefineDynamicAssembly(
              name, AssemblyBuilderAccess.RunAndSave);
            //Для порождения входящих в сборку модулей 
            ModuleBuilder module = builder.DefineDynamicModule
              (modName, true);
            //Для класса задаем имя класса и атрибут доступа 
            TypeBuilder typeBuilder = module.DefineType(typeName,
              TypeAttributes.Public);
            //создпем метод с ооответствующими парпметрами
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(
                            assemblyName,
                            MethodAttributes.Public,
                            typeof(uint),
                            new Type[] { typeof(uint), typeof(uint) }
                            );

            ILGenerator iLGenerator = methodBuilder.GetILGenerator();
            iLGenerator.Emit(OpCodes.Ldarg_1);
            iLGenerator.Emit(OpCodes.Ldarg_2);
            iLGenerator.Emit(OpCodes.Xor);
            iLGenerator.Emit(OpCodes.Ret);
            // завершает создание типа
            typeBuilder.CreateType();

            builder.Save(assemblyName + ".dll");
        }

    }
}
