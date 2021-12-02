using crypto;

public class Elgamal{
    public static void Main(){
        String input;
        List<char> Clist=new List<char>();
        List<int> crypto_en=new List<int>();
        List<char> crypto_de=new List<char>();
        keys Key=new keys(257,"private");

        Crypto message =new Crypto(Key);
        while(true){
            try{
                System.Console.Write("Encode mode:");
                input = System.Console.ReadLine();
                break;
            }catch(System.FormatException){
                System.Console.WriteLine("error");
            }
        }
        Clist=Crypto.StoCList(input);

        System.Console.WriteLine(Key.Output_publickey());

        message.Encrypto_and_save(Clist);
        crypto_en=message.Get_c2();
        Write_intList(crypto_en,"char");

        System.Console.WriteLine(message.Output_crypto());
        
        crypto_de=message.Decrypto(crypto_en);
        
        Write_charList(crypto_de,"char");

    }

    private static void Write_intList(List<int> clist,String mode){
        foreach(char item in clist){
            if(mode=="char"){
                System.Console.Write((char)item);
            }else if(mode=="num"){
                System.Console.Write("{0},",item);
            }else{
                break;
            }
        }
        System.Console.WriteLine();
    }

    private static void Write_charList(List<char> clist,String mode){
        foreach(char item in clist){
            if(mode=="char"){
                System.Console.Write((char)item);
            }else if(mode=="num"){
                System.Console.Write("{0},",item);
            }else{
                break;
            }
        }
        System.Console.WriteLine();
    }
}