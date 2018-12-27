using System;

namespace ConsoleAppNewGrammar
{
    class Program
    {
        static void Main(string[] args)
        {
            Out();
            Console.ReadKey();
        }


        static void Out()
        {
            //旧语法
            string val = "10";
            int result;
            int.TryParse(val, out result);
            Console.WriteLine(result);

            //新语法
            int.TryParse(val, out int res);
            Console.WriteLine(res);
        }

        //旧语法
        static Tuple<bool, string> TupleOldFun()
        {
            Tuple<bool, string> t = new Tuple<bool, string>(true, "ok");
            return t;
        }
        //新语法
        static (bool, string) TupleNewFun()
        {
            return (true, "ok");
        }

        static void UseTuple()
        {
            //旧语法
            var t = TupleOldFun();
            var isOK = t.Item1;
            var Msg = t.Item2;

            //新语法
            var (isOK1, OKMsg) = TupleNewFun();
        }
    }
}
