using private_roots;

namespace crypto{
    public class keys{
        private string Type;
        private int g;
        private int y;
        private int x;
        private int p;
        private Random seed=new Random();
        private int r;
        
        public keys(int p,String type){
            while(true){
                if(Set_keys(p))break;
                p+=2;
            }
            Set_Type(type);
            Set_r();
        }
        private bool Set_keys(int p){
            if(!proots.prime(p))    return(false);
            this.p=p;
            this.x=seed.Next(0,this.p-2);
            this.g=proots.roots_v2(this.p)[seed.Next(0,proots.roots_v2(this.p).Count-1)];
            this.y=proots.roots_math(this.g,this.x,this.p);
            return(true);
        }
        private bool Set_Type(String type){
            if(type=="private"){
                this.Type=type;
                return(true);
            }else if(type=="public"){
                this.Type=type;
                return(true);
            }else{
                return(false);
            }
        }
        private void Set_r(){
            this.r=seed.Next(0,this.p-2);
        }
        public int Get_r(){
            return(this.r);
        }
        public int Get_x(){
            if(this.Type!="private") return(-1);
            return(this.x);
        }
        public int[] Get_publickey(){
            int[] key={this.p,this.g,this.y};
            return(key);
        }
        public String Output_publickey(){
            return("{"+this.p.ToString()+","+this.g.ToString()+","+this.y.ToString()+"}");
        }
        public String Get_Type(){
            return(this.Type);
        }
    }

    public class Crypto{
        private int c1;
        private List<char> origin=new List<char>();
        private List<int> c2=new List<int>();
        private keys Key;

        public Crypto(keys Key){
            this.Key=Key;
            Set_c1();
        }
        private void Set_c1(){
            this.c1=proots.roots_math(this.Key.Get_publickey()[1],this.Key.Get_r(),this.Key.Get_publickey()[0]);
        }
        public void Encrypto_and_save(List<char> origin){
            this.c2=Encrypto(origin);
        }
        public void Decrypto_and_save(List<int> c2){
            this.origin=Decrypto(c2);
        }
        public List<int> Encrypto(List<char> origin){
            this.origin=origin;
            List<int> c2t=new List<int>();
            foreach(char m in origin){
                c2t.Add(Encrypto_math(m,this.Key));
            }
            return(c2t);
        }
        public List<char> Decrypto(List<int> c2){
            if(this.Key.Get_Type()=="public") return(null);
            this.c2=c2;
            List<char> origint=new List<char>();
            foreach(int item in c2){
                origint.Add(Decrypto_math(item,this.c1,this.Key));
            }
            return(origint);
        }
        public static int Encrypto_math(int m,keys public_key){
                int i=public_key.Get_publickey()[2];//y
                int n=public_key.Get_r();
                int num=public_key.Get_publickey()[0];//p

                double log2 = Math.Log(i,2);
                int t;
                if(Math.Ceiling(n*log2)>=53){
                    double n2 = (double)n/2;
                    int it=proots.roots_math(i,(int)Math.Ceiling(n2),num);
                    int nt=proots.roots_math(i,(int)Math.Floor(n2),num);
                    
                    t=(m*it*nt)%num;
                }else{
                    t=(int)((ulong)Math.Pow(i,n)*(ulong)m % (ulong)num);
                }
                //System.Console.WriteLine("Encrypto_math:{0},{1}",t,(char)t);
                return(t);
            }
        public static char Decrypto_math(int c2,int c1,keys private_key){
                int i=c1;
                int num=private_key.Get_publickey()[0];//p
                int n=num-1-private_key.Get_x();
                double log2 = Math.Log(i,2);
                char m;
                if(Math.Ceiling(n*log2)>=53){
                    double n2 = (double)n/2;
                    int it=proots.roots_math(i,(int)Math.Ceiling(n2),num);
                    int nt=proots.roots_math(i,(int)Math.Floor(n2),num);
                    
                    m=(char)((c2*it*nt)%num);
                }else{
                    m=(char)(((ulong)Math.Pow(i,n)*(ulong)c2) % (ulong)num);
                }
                return(m);
            }
        public static List<char> StoCList(String input){
            List<char> CList=new List<char>();
            foreach(char item in input){
                CList.Add(item);
            }
            return(CList);
        } 
        public List<char> Get_origin(){
            return(this.origin);
        }
        public List<int> Get_c2(){
            return(this.c2);
        } 
        public void all_output(){
            System.Console.WriteLine("-All Output-");
            System.Console.WriteLine("C1:{0}",this.c1);
        }
        public String Output_crypto(){
            String crypto="["+this.c1.ToString()+",{";
            int i=0;
            foreach(char item in this.c2){
                crypto+=((int)item).ToString();
                if(i!=this.c2.Count()) crypto+=",";
                i++;
            }
            crypto+="}]";
            return(crypto);
        }
    }
}
