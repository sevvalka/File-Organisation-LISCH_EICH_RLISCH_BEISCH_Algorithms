using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LISCH_EICH_RLISCH__BEISCH_Algorithms
{
    public partial class Form3 : Form
    {
        int number;  // array uzunluğu
        int N; // Tablo index sayısı
        int[] random_array; // rastgele dizi
        bool found=false;   // arama için bulundu bayrağı
        public Form3(int number)
        {
            InitializeComponent();
            this.number = number;
           

        }

        public void Form3_Load(object sender, EventArgs e)
        {
            label22.Text = "Tüm tablo kaç adımda \n yerleşti";
            timer1.Enabled = true;
            Primary();
            label20.Text = number.ToString();
            random_array = new int[number];
            Random random = new Random();
            for (int i = 0; i < number; i++)
            {
                random_array[i] = random.Next(1, 1000);
                listBox1.Items.Add(i + ". " + random_array[i] + " (mod: " + N +") "+ random_array[i]%N);
            }
            
            eich();
            lisch();
            rlisch();
            beisch();

        }
        static int probe = 0;
        int Ara(int target,int []hafiza, int[] link)
        {
            
            for (int i = 0; i < N; i++)
            {
                if (target == hafiza[i])
                {
                    found = true;
                    
                }
            }
            if (found == false)
                probe=-1;
            if (found == true)
          {
                int home_adr = target % N;
                if (target == hafiza[home_adr])
                {
                    probe = 1;
                }
                else if (target != hafiza[home_adr])
                {
                    probe=2;
                    while (target != hafiza[link[home_adr]])
                    {
                        probe = probe + 1;
                        home_adr = link[home_adr];
                        
                        
                    }
                }
                
            }
            found = false;
            return probe;
           
        }

        
        int beisch_counter=0;
        int[] beisch_addr;
        int[] beisch_link;
        public void beisch()
        {
            beisch_addr = new int[N];
            beisch_link = new int[N];
            bool turn = false;
            // true --> üst,top , false --> alt
            for (int i = 0; i < N; i++)
            {
                beisch_addr[i] = -1;
                beisch_link[i] = -1;
            }
            int home_address;  // anahtarın adres değeri
            int son_index = N - 1; // tablodaki en son (boş) index
            int top = 0; // tablonun en başındaki (boş) index
            for (int i = 0; i < random_array.Length; i++)
            {
                beisch_counter++;
                home_address = random_array[i] % (N);  // home adresini hesapla
                if (beisch_addr[home_address] == -1)//home adres boş ise, key oraya yazılır
                {
                    beisch_counter++;
                    beisch_addr[home_address] = random_array[i];

                }
                else// home adres doluysa
                {
                    beisch_counter++;
                    if (beisch_link[home_address] == -1)  //adres dolu fakat dolu linki boşsa
                    {
                        beisch_counter++;
                        if (turn == true)  // üste yazma sırası gelmiş ise
                        {
                            beisch_counter++;
                            if (beisch_addr[top] == 0) // en üst boş ise yazılır.
                            {
                                beisch_counter++;
                                beisch_addr[top] = random_array[i];
                                beisch_link[home_address] = top;
                            }
                            else //en üst boş değil ise
                            {
                                beisch_counter++;
                                while (beisch_addr[top] != -1) // en üstten aşağıya doğru inilerek, en üstte olan boş index bulunur
                                {
                                    beisch_counter++;
                                    top = top + 1;
                                }
                                beisch_addr[top] = random_array[i];
                                beisch_link[home_address] = top;
                            }
                            turn = false;
                        }

                        else if (turn == false) // turn=true durumunun tam tersi, adres doluysa linkin göstereceği adres en alttan aranmaya başlanır.
                        {
                            beisch_counter++;
                            if (beisch_addr[son_index] == 0)
                            {
                                beisch_counter++;
                                beisch_addr[son_index] = random_array[i];  
                                beisch_link[home_address] = son_index;
                            }
                            else //boş değilse
                            {
                                beisch_counter++;
                                while (beisch_addr[son_index] != -1)
                                {
                                    beisch_counter++;
                                    son_index = son_index - 1;
                                }
                                beisch_addr[son_index] = random_array[i];
                                beisch_link[home_address] = son_index;                   
                            }
                            turn = true;
                        }
                    }
                    else if (beisch_link[home_address] != -1)//home adres dolu linki de dolu ise,linki boş olanı bulana kadar git
                    {
                        
                        beisch_counter++;
                        if (turn == false)
                        {
                            beisch_counter++;
                            int temp = beisch_link[home_address];
                            while (beisch_addr[son_index] != -1)
                            {
                                son_index--;
                                beisch_counter++;
                            }
                            // early insertionda zincire eklenen 3. veya sonraki değerleri, zincirin ilk halkası gösterir.(zincirin ikinci halkasına eklenir)
                            beisch_addr[son_index] = random_array[i];
                            beisch_link[home_address] = son_index;
                            beisch_link[son_index] = temp;
                            turn = true;
                        }
                        else if (turn == true)
                        {
                            beisch_counter++;
                            int temp = beisch_link[home_address];
                            while (beisch_addr[top] != -1)
                            {
                                beisch_counter++;
                                top++;
                            }
                            beisch_addr[top] = random_array[i];
                            beisch_link[home_address] = top;
                            beisch_link[top] = temp;
                            turn = false;
                        }
                    }
                }
            }
            for (int i = 0; i < N; i++)
            {

                Beisch_txtbox.Items.Add((i).ToString() + ".  " + beisch_addr[i].ToString() + " |  " + beisch_link[i].ToString());
            }
            label23.Text = beisch_counter.ToString();
        }



        
        int lisch_counter = 0;
        int[] lisch_addr;
        int[] lisch_link;
        public void lisch()
        {
            lisch_addr=new int[N];
            lisch_link=new int[N];
            for (int i = 0; i < N; i++)
            {
                lisch_addr[i] = (-1);
                lisch_link[i] = (-1);
            }
            int home_address;
            int son_index = N-1;
            for (int i = 0; i < random_array.Length; i++)
            {
                lisch_counter++;
                home_address = random_array[i] % (N);
                if (lisch_addr[home_address] == (-1)) // home adresi boş ise
                {
                    lisch_counter++;
                    lisch_addr[home_address] = random_array[i];

                }
                else // home adresi boş değil ise
                {
                    lisch_counter++;
                    if (lisch_link[home_address] == (-1)) // adres dolu linki boş  
                    {
                        lisch_counter++;
                        if (lisch_addr[son_index] == (-1)) // lischte yerleştirilmeye en alttan başlanır, en alt boş ise
                        {
                            lisch_counter++;
                            lisch_addr[son_index] = random_array[i];  
                            lisch_link[home_address] = son_index;
                        }
                        else // en alt boş değil ise boş yer bulunana kadar en alttan yukarı doğru bakılır.
                        {
                            lisch_counter++;
                            while (lisch_addr[son_index] != (-1))
                            {
                                lisch_counter++;
                                son_index = son_index - 1;
                            }
                            lisch_addr[son_index] = random_array[i];
                            lisch_link[home_address] = son_index;                  
                        }
                    }
                    else // adres dolu linki de dolu
                    {
                        lisch_counter++;
                        while (lisch_link[home_address] != (-1)) // boş link bulunur.
                        {
                            lisch_counter++;
                            home_address = lisch_link[home_address];     
                        }                
                        if (lisch_addr[son_index] == (-1)) 
                        {
                            lisch_counter++;
                            lisch_addr[son_index] = random_array[i];   
                            lisch_link[home_address] = son_index;
                        }
                        else 
                        {
                            lisch_counter++;

                            while (lisch_addr[son_index] != (-1))
                            {
                                lisch_counter++;
                                son_index = son_index - 1;
                            }
                            lisch_addr[son_index] = random_array[i];
                            lisch_link[home_address] = son_index;
                           
                        }
                      
                    }
                }
            }
            for (int i = 0; i < N; i++)
            {
                Lisch_txtbox.Items.Add((i).ToString() + ".  " + lisch_addr[i].ToString() + " |  " + lisch_link[i].ToString());
            }
            label26.Text = lisch_counter.ToString();
        }
    
        int eich_counter = 0;
        int[] eich_addr;
        int[] eich_link;
        public void eich()
        {
            eich_addr = new int[N];
            eich_link = new int[N];
            for(int i=0;i<N;i++)
            {
                eich_addr[i] = (-1);
                eich_link[i] = (-1);
            }
            int home_address;  
            int son_index = N-1;
            for (int i = 0; i < random_array.Length; i++)
            {
                eich_counter++;
                home_address = random_array[i] % (N);  
                if (eich_addr[home_address] == (-1)) // home adres boş ise oraya yaz
                {
                    eich_counter++;
                    eich_addr[home_address] = random_array[i];

                }
                else // home adres dolu ise
                {
                    eich_counter++;
                    if (eich_link[home_address] == (-1)) //home adres dolu linki boş  
                    {
                        eich_counter++;
                        if (eich_addr[son_index] == (-1)) // home adres dolu linki boş, en alt adres boş
                        {
                            eich_counter++;
                            eich_addr[son_index] = random_array[i];   
                            eich_link[home_address] = son_index;
                        }
                        else //en alt adres boş değil ise boş yer bulunana kadar teker teker aşağıdan yukarı bakılır
                        {
                            eich_counter++;
                            while (eich_addr[son_index] != (-1))
                            {
                                eich_counter++;
                                son_index = son_index - 1;
                            }
                            eich_addr[son_index] = random_array[i];
                            eich_link[home_address] = son_index;                   
                        }
                    }
                    else if (eich_link[home_address] != (-1))//adres dolu linki de dolu ise linki boş olanı bulana kadar git
                    {
                        eich_counter++;
                        int temp = eich_link[home_address];
                        while (eich_addr[son_index] != (-1))
                        {
                            eich_counter++;
                            son_index--;
                        }
                        // early insertion
                        eich_addr[son_index] = random_array[i];
                        eich_link[home_address] = son_index;
                        eich_link[son_index] = temp;



                    }
                }
            }
            for(int i=0;i<N;i++)
            {  if (i == number)
                    Eich_txtbox.Items.Add("-------cellar----");
               Eich_txtbox.Items.Add((i).ToString() + ".  " + eich_addr[i].ToString() + " |  " + eich_link[i].ToString());
            }
            label25.Text = eich_counter.ToString();
        }
        
           int rlisch_counter = 0;
           int[] rlisch_addr;
           int[] rlisch_link;
           public void rlisch()
           {
               rlisch_addr = new int[N];
               rlisch_link = new int[N];
            for (int i = 0; i < N; i++)
            {
                rlisch_addr[i] = (-1);
                rlisch_link[i] = (-1);
            }
               Random rand = new Random();
               int home_address;
               int random_index=rand.Next(0,N-1); // rastgele üretilen adres indexi
               for (int i = 0; i < random_array.Length; i++)
               {
                rlisch_counter++;
                home_address = random_array[i] % (N);
                   if (rlisch_addr[home_address] == (-1)) // home adresi boş ise oraya anahtarı yaz
                   {
                    rlisch_counter++;
                    rlisch_addr[home_address] = random_array[i];

                   }
                   else if (rlisch_addr[home_address] != (-1)) // home adresi dolu ise
                    {
                    rlisch_counter++;
                    if (rlisch_link[home_address] == (-1))  // adres dolu linki boş ise
                    {
                        rlisch_counter++;                     
                            while (rlisch_addr[random_index] != (-1)) // rlischte en son veya en alttan boş adres aranmaz, indexler rastgele aranır
                               {
                                  rlisch_counter++;
                                  random_index = rand.Next(0, N-1);
                                }


                             rlisch_addr[random_index] = random_array[i];   
                             rlisch_link[home_address] = random_index;
                     }


                     else if (rlisch_link[home_address] != (-1)) // linki de dolu ise
                    {
                        rlisch_counter++;
                        while (rlisch_link[home_address] != (-1)) //boş link aranır
                        {
                            rlisch_counter++;
                            home_address = rlisch_link[home_address];
                        }
                        while (rlisch_addr[random_index] != -1) // boş link bulununca, boş adres rastgele bulunana kadar aranır
                        {
                            rlisch_counter++;
                            random_index = rand.Next(0, N - 1);
                        }

                        rlisch_addr[random_index] = random_array[i];  
                        rlisch_link[home_address] = random_index;

                    }
                        
                    }
                

            }
            for (int i = 0; i < N; i++)
            {
                
                Rlisch_txtbox.Items.Add((i).ToString() + ".  " + rlisch_addr[i].ToString() + " |  " + rlisch_link[i].ToString());
            }
            label24.Text = rlisch_counter.ToString();

        }
           
        public void Primary()
        {
            // dizi sayısından büyük, package factoru %95'ten küçük, en küçük asal sayı arama metodu
            N=number+1;
            check:
            for (int i = 2; i < number; i++)
            {
                int kalan = N % i;
                if (kalan == 0)
                {
                    N++;
                    goto check;
                }
               
            }
           
            if((float)number / (float)N >0.95)
            {
                N++;
                goto check;
            }
            float pac_fac = (float)number / (float)N;
            label4.Text = N.ToString();
            label5.Text = pac_fac.ToString();
           

        }

        private void timer1_Tick(object sender, EventArgs e)
        {


            label18.Text = label18.Text.Substring(1) + label18.Text.Substring(0, 1);
        }
       


        private void button1_Click(object sender, EventArgs e)
        {
            int target = Convert.ToInt32(textBox1.Text);
            int lisch_probe,beisch_probe,rlisch_probe,eich_probe;
            lisch_probe=Ara(target, lisch_addr, lisch_link);
         
           beisch_probe = Ara(target, beisch_addr, beisch_link);
         
          rlisch_probe = Ara(target, rlisch_addr, rlisch_link);
         
          eich_probe = Ara(target, eich_addr, eich_link);
            
           label14.Text = (" " + beisch_probe.ToString());
           label15.Text = (" " + rlisch_probe.ToString());
           label16.Text = (" " + eich_probe.ToString());
           label17.Text = (" " +lisch_probe.ToString());
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Close();
        }
    }
}
