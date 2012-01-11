using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Diagnostics;

namespace kpcc
{
    class Program
    {
        struct _argv
        {
            string compile;
            string library;
            string legal;
            string other;
        }

        static void Main(string[] args)
        {
          
            if (args.Length < 1)
            {
                System.Console.Write("KPCC -- Krypton-Project C-Sharp Compiler\n===========================================\n./args\n");
                System.Console.WriteLine("-------------------------------------------");
                System.Console.Write("-c <file>\tCompile Single file\n");
                System.Console.Write("-d <file>\tCompile to Class Library\n");                
                System.Console.WriteLine("-------------------------------------------");
                System.Console.ReadLine();
            }

            else
            {
                if (args[0] == "-c" && args.Length == 2)
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(args[1]))
                    {
                        string filetext = sr.ReadToEnd();
                        sr.Dispose();
                        CSharpCodeProvider codeProvider = new CSharpCodeProvider();
                        
                        ICodeCompiler compiler = codeProvider.CreateCompiler();
                        CompilerParameters param = new CompilerParameters();

                        param.GenerateExecutable = true;

                        param.OutputAssembly = "a.exe"; 
                        foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
                        {
                            param.ReferencedAssemblies.Add(asm.Location);
                        }
                        String code = filetext.ToString();
                        CompilerResults results = compiler.CompileAssemblyFromSource(param,
                                                                 code);                        
                        if (results.Errors.Count != 0)
                        {
                            /* AN error has occured! */
                            string errors = "Compilation failed:\n";
                            foreach (CompilerError _er_ in results.Errors)
                            {
                                errors += _er_.ToString() + "\n";
                            }

                            System.Console.WriteLine(errors);
                            System.Console.Beep();
                        }
                    }
                }

                else if (args[0] == "-d" && args.Length == 2)
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(args[1]))
                    {
                        string filetext = sr.ReadToEnd();
                        sr.Dispose();
                        CSharpCodeProvider codeProvider = new CSharpCodeProvider();

                        ICodeCompiler compiler = codeProvider.CreateCompiler();                       
                        CompilerParameters param = new CompilerParameters();
                        param.GenerateExecutable = true;

                        param.OutputAssembly = "a.dll";
                        foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
                        {
                            param.ReferencedAssemblies.Add(asm.Location);
                        }
                        String code = filetext.ToString();
                        CompilerResults results = compiler.CompileAssemblyFromSource(param,
                                                                 code);
                        
                        if (results.Errors.Count != 0)
                        {
                            /* AN error has occured! */
                            string errors = "Compilation failed:\n";
                            foreach (CompilerError _er_ in results.Errors)
                            {
                                errors += _er_.ToString() + "\n";
                            }

                            System.Console.WriteLine(errors);
                            System.Console.Beep();
                        }
                    }
                }
                else System.Console.WriteLine("Syntax Error");
            }
        }
    }
}
