namespace BooleanFunctionFunction;

using System;
using static System.Convert;
using static System.Console;

public delegate bool BoolFunction(bool A, bool B, bool C);

public class NotBinaryException : Exception{
    public NotBinaryException(string message):base(message){}
}

class Program
{
    public static BoolFunction SOPGenerator(byte[] occurences){
        return (bool A, bool B, bool C) =>{// --->>--> <=

            bool result = false;

            foreach (var occ in occurences)
            {
                if (occ>7)
                {
                    return false;
                }
                
                bool fourths = occ/4 >= 1 ? true : false;
                bool twos = (occ-ToByte(fourths)*4)/2 >= 1 ? true : false;
                bool ones = ToBoolean(occ%2);
                bool _A = A==fourths ? true : false;
                bool _B = B==twos ? true : false;
                bool _C = C==ones ? true : false;
                result = result || (_A&&_B&&_C);
            }

            return result;
        };
    }

    static void ValidateBinary(string line){
        foreach (var letter in line)
            {
                if(Int16.Parse($"{letter}")>1){
                    throw new NotBinaryException("The number is not binary!");
                }
            }
    }

    static void Main(string[] args)
    {
        byte[] arr = {1,3,5};
        BoolFunction myBoolFunc = SOPGenerator(arr); 
        string? strInp = ReadLine();
        string line = strInp != null ? strInp : "0";
        do
        {
            try{
                ValidateBinary(line);
                int inp = line != null? Int16.Parse(line) : 0;
                if(inp<=111){
                    bool A = ToBoolean(inp/100);
                    bool B = ToBoolean((inp - ToInt32(A)*100)/10);
                    bool C = ToBoolean(inp%10);
                    WriteLine(myBoolFunc(A,B,C));
                }
                else{
                    WriteLine("Please enter only three digits binary numbers");
                }
            }
            catch(FormatException){
                WriteLine("Can't parse this!");
            }
            catch(OverflowException e){WriteLine(e.Message);}
            catch(NotBinaryException e){WriteLine(e.Message);}

            strInp = ReadLine();
            line = strInp != null ? strInp : "0";
        }
        while(line != "exit");
    }

}
