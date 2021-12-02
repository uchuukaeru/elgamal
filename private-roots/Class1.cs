namespace private_roots{
    public class proots{
            private static List<int> roots(int num){
                List<int> roots=new List<int>();
                int numt=num-1;

                for(int i=0;i<numt;i++){
                    int[] test = new int[numt];
                    int it=i+1;
                    for(int n=0;n<numt;n++){
                        int nt=n+1;
                        //System.Console.WriteLine("i:{0},n:{1},num:{2}--{3},{4}",it,nt,num,(ulong)Math.Pow(it,nt),(ulong)Math.Pow(it,nt)%(ulong)num);
                        int m=(int)((ulong)Math.Pow(it,nt) % (ulong)num);
                        if(m==0) return(roots);
                        test[n]=m;
                    }
                    //proots.ArrayWriteLine_int(test);
                    if(dup(test))    roots.Add(it);
                }
                return(roots);
            }

            public static List<int> roots_v2(int num){
                List<int> roots=new List<int>();
                int numt=num-1;

                for(int i=0;i<numt-1;i++){
                    int[] test = new int[numt];
                    int it=i+2;
                    for(int n=0;n<numt;n++){
                        int nt=n+1;
                        //System.Console.Write("call from roots_ob_v2,");
                        int m = roots_math(it,nt,num);
                        //System.Console.WriteLine("i:{0},n:{1},num:{2}--{3},{4}",it,nt,num,(ulong)Math.Pow(it,nt),(ulong)Math.Pow(it,nt)%(ulong)num);
                        test[n]=m;
                    }
                    //proots.ArrayWriteLine_int(test);
                    if(dup(test))    roots.Add(it);
                }
                return(roots);
            }

            public static List<int> roots_v3(int num){
                List<int> roots=new List<int>();
                int numt=num-1;

                for(int i=0;i<numt-1;i++){
                    int[] test = new int[numt];
                    int it=i+2;
                    bool flag=true;
                    for(int n=0;n<numt;n++){
                        int nt=n+1;
                        //System.Console.Write("call from roots_ob_v2,");
                        int m = roots_math(it,nt,num);
                        //System.Console.WriteLine("i:{0},n:{1},num:{2}--{3},{4}",it,nt,num,(ulong)Math.Pow(it,nt),(ulong)Math.Pow(it,nt)%(ulong)num);
                        if(!dup_in_array(test,m)){
                            flag=false;
                            break;
                        }
                        test[n]=m;
                    }
                    //ArrayWriteLine_int(test);
                    //proots.ArrayWriteLine_int(test);
                    if(flag)    roots.Add(it);
                }
                return(roots);
            }

            public static int roots_math(int i,int n,int num){
                double log2 = Math.Log(i,2);
                int m;
                if(Math.Ceiling(n*log2)>=53){
                    double n2 = (double)n/2;
                    int it=roots_math(i,(int)Math.Ceiling(n2),num);
                    int nt=roots_math(i,(int)Math.Floor(n2),num);
                    
                    m=(it*nt)%num;
                }else{
                    m=(int)((ulong)Math.Pow(i,n) % (ulong)num);
                }
                return(m);
            }


            public static bool prime(int num){
                for(int i=2;i<num;i++)  if(num%i==0)    return(false);
                return(true);
            }

            private static bool dup(int[] test){
                HashSet<int> set=new HashSet<int>();
                foreach(int DupList in test){
                    if(DupList == 0) continue;
                    if(!set.Add(DupList))   return(false);
                }
                return(true);
            }

            private static bool dup_in_array(int[] test,int num){
                HashSet<int> set=new HashSet<int>();
                foreach(int DupList in test){
                    if(DupList == 0) break;
                    if(!set.Add(DupList))   return(false);
                }
                if(!set.Add(num))   return(false);
                return(true);
            }
        }
}