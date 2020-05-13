using System;

namespace CuratorProject
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                int option = displayMenu();
                switch (option)
                {
                    case 1:
                        addCurator();
                        break;
                    case 2:
                        addArtist();
                        break;
                    case 3:
                        addPiece();
                        break;
                    case 4:
                        Inventory();
                       break;
                    case 5:
                        sellPiece();
                        break;
                    case 6:
                        ReturnItem();
                        break;
                    case 7:
                        Environment.Exit(0);
                        break;
                }
                Console.WriteLine("*********************************************************************");
            }
        }
        public static bool checkStatus(string id)
        {
            bool flag = false;
            foreach (artPieces art in MyArtPieces) {
                if (art.status == 'S')
                    flag = true;
            }
            return flag;
        }

        public static void ReturnItem(){
        Console.WriteLine("Please enter the ID of your current artPiece");
        string artID = Console.ReadLine();
        if (CheckArtID(artID) == true)
        {
            Console.WriteLine("Please enter the new art piece ID you wish to purchase");
            string Newid = Console.ReadLine();
            if (CheckArtID(Newid) == true) {
                double pre_val = returnItemValue(artID);
                double next_val = returnItemValue(Newid);
                if (checkStatus(artID)==true && pre_val > next_val)
                {
                    Console.WriteLine("Item has been replcaed successfully");
                    UpdateStatus(Newid);
                    changeStatus(artID);
                    Console.WriteLine("You will be reimpbursed " + (pre_val-next_val));
                    // change status
                }
                else if (checkStatus(artID) == true && pre_val < next_val) {
                    Console.WriteLine("Your item has been exchanged");
                    UpdateStatus(Newid);
                    changeStatus(artID);
                    Console.WriteLine("you need to pay "+ (next_val-pre_val));
                    // Adam code here
                }
                else if(checkStatus(artID) == true && pre_val == next_val) {
                        UpdateStatus(Newid);
                        changeStatus(artID);
                        Console.WriteLine("Item has been replcaed successfully");
                    // Adam code here
                }
            }

        }
           
            // (DONE) - check for the validity of this ID;
            // (DONE) read the ID of his next artpice
            // if it exist, you need to comapre their values
            // if the new item's value is mre than previous one, deduct values and compute the remaining value which he shoudl pay
            //otherwise dedcut value of the second from the first, and pay the remaining to him
            // of values are equal, then accept replacement and then changes the status.
            //note: change status has two pasts. part1: change the status of the sold item to 'D' and part 2. chnage the status of the new item to 'S'
        }
        public static double returnItemValue(string ID) {
            double value = 0;
            for (int i = 0; i < MyArtPieces.Length; i++) {
                if (MyArtPieces[i].artId == ID) {
                    value = MyArtPieces[i].value;
                }
            }
            return value;
        }
     
        public static bool CheckArtID(string ID)
        {
            bool flag = false;
            foreach (artPieces art in MyArtPieces) {
                if (art.artId == ID)
                    flag = true;
            }
            return flag;
        }
      

        public static void UpdateStatus(string id)
        {
            for (int k = 0; k < MyArtPieces.Length; k++)
                if (MyArtPieces[k].artId == id)
                {
                    MyArtPieces[k].status = 'S';
                }

        }

        public static int displayMenu() {
            Console.WriteLine("1. Add curator");
            Console.WriteLine("2. Add artist");
            Console.WriteLine("3. Add artpiece");
            Console.WriteLine("4. Inventory report");
            Console.WriteLine("5. Sell artipiece");
            Console.WriteLine("6. Return Item");
            Console.WriteLine("7. Exit");

            int select = Convert.ToInt32(Console.ReadLine());
            return select;
        }

        static Curator[] myCuratorList = new Curator[10]; // 10 records, every record 4 fields or properties                                              
        public static int index_c = 0;

        struct Curator
        {
            public string firstName;
            public string lastName;
            public string curatorID;
            public double commission;
        }
       
        public static void addCurator()
        {
            Console.WriteLine("Please enter your curator ID: ");
            string tempID = Console.ReadLine();

            if (tempID.Length != 5)
            { // if the length of tempID is not equal to five: 
                Console.WriteLine("Error.Your curator ID should be exactly five characters");
            }
            else if (curatorIDValidator(tempID) == true) {
                Console.WriteLine("There exsit a curator with this ID.");
            }
            else
            {
                //curatorID = tempID;
                myCuratorList[index_c].curatorID = tempID;


                Console.WriteLine("Please enter your firstname: ");
                string tempFisrstName = Console.ReadLine();
                Console.WriteLine("Please enter your lastname: ");
                string tempLastname = Console.ReadLine();
                if ((tempFisrstName.Length + tempLastname.Length) > 30)
                {
                    Console.WriteLine("Error. Name should be less than or equal to 30 characters");
                }
                else
                {
                    //firstName = tempFisrstName;
                    myCuratorList[index_c].firstName = tempFisrstName;
                    //lastName = tempLastname;
                    myCuratorList[index_c].lastName = tempLastname;
                }
                index_c++;

                double monthlySales;
                double commission;
                int x = 100;
                double result;
                Console.WriteLine("Please enter your monthly sales: If 100$ enter 100");
                monthlySales = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter your commission rate: If 25% enter 25");
                commission = Convert.ToDouble(Console.ReadLine());
                result = (commission * monthlySales / x);
                Console.WriteLine();
                Console.WriteLine("Your commission rate is: $ {0}", result);
                Console.ReadLine();
            }
        }
        // implement a method wich returns list of curator and list of artistis with all their detail information.
        // These are information of curator list:
        // INFO
        // These are information of artists list:
        // INFO 
        public static void curatorArtistLists() {
            Console.Write("INFO for Curator");
            foreach (Curator cur in myCuratorList) {
                Console.WriteLine(cur.curatorID+ " "+cur.firstName+ " " + cur.lastName+ " " + cur.commission);
            }
            Console.WriteLine("INFO for Artist:");
            foreach (Artist art in myArtists) {
                Console.WriteLine(art.artistFirstName + " " + art.artistID + " " + art.artistLastName + " " + art.refCurator);
            }
        }

        public static bool curatorIDValidator(string ID) {
            bool flag = false;
            foreach (Curator cur in myCuratorList) {
                if (cur.curatorID == ID)
                    flag = true;
            }
            return flag;
        }
        
        public static bool ArtistIDValidation(string ID)
        {
             bool flag = false;
            foreach(Artist art in myArtists)
            {
                if (art.artistID == ID)
                    flag = true;
            }
            return flag;
        }
        static Artist[] myArtists = new Artist[10];
        public static int index_a = 0;
        struct Artist
        {

            public string artistFirstName;
            public string artistLastName;
            public string artistID;
            public string refCurator;
        }
        public static void addArtist()
        {


            Console.WriteLine("Please enter your first name: ");
            Console.ReadLine();
            Console.WriteLine("Please enter your last name: ");
            Console.ReadLine();

            Console.WriteLine("Please enter your artist ID:");
            string tempArtistId = Console.ReadLine();
            if (tempArtistId.Length != 5)
            {
                Console.WriteLine("Error. artist Id must be 5 characters in lenght");
             
            }else if(ArtistIDValidation(tempArtistId) == true)
            {
                Console.WriteLine("An artist with this name already exists");
            }
            else
            {
                myArtists[index_a].artistID = tempArtistId;
                // tempArtistId = artistID;

                index_a++;
                Console.WriteLine("Please enter your curator specific ID: ");
                Console.ReadLine();

            }
        }
        
      
 
        static artPieces[] MyArtPieces = new artPieces[10];
        public static int index_b = 0;
        struct artPieces
        {
            public string artId;
            public char status;
            public string artistID;
            public double value;
            public int year;
            public string title;
        }
        public static void addPiece()
        {
      
            Console.WriteLine("Please enter your art ID: ");
            string tempArtId = Console.ReadLine();
            if (tempArtId.Length != 5)
            {
                Console.WriteLine("Error.Your art piece Id must be 5 characters in length");

            }
            else if (artPieceValidator(tempArtId) == true) {
                Console.WriteLine("This id for artpiece already exsist.");
            }
            else
            {
                MyArtPieces[index_b].artId = tempArtId;

                //tempArtId = artId;
            

            Console.WriteLine("Please enter the name of your art piece: ");
            string tempPieceName = Console.ReadLine();
                if (tempPieceName.Length > 40)
                {
                    Console.WriteLine("Error. Your art piece name should not exceed 40 characters");
                }
                else
                {
                    MyArtPieces[index_b].title = tempPieceName;


                    Console.WriteLine("Please enter your arist Id: ");
                    string _artistID = Console.ReadLine();
                    MyArtPieces[index_b].artistID = _artistID;


                    Console.WriteLine("The value of your art piece");
                    double _value = Convert.ToDouble(Console.ReadLine());
                    MyArtPieces[index_b].value = _value;

                    Console.WriteLine("Please enter the year for the art piece: ");
                    int _year = Convert.ToInt32(Console.ReadLine());
                    MyArtPieces[index_b].year = _year;
                    MyArtPieces[index_b].status = 'D';
                    index_b++;
                }
            }
        }
        public static bool artPieceValidator(string ID) {
            bool flag = false;
            foreach (artPieces piece in MyArtPieces) {
                if (piece.artId == ID) {
                    flag = true;
                }
            }
            return flag;
        }
    
        public static void Inventory()
        {
            foreach (artPieces piece in MyArtPieces) {
                if (piece.artId != null)
                {
                    Console.WriteLine(piece.artId + " " + piece.artistID + " " + piece.title + " " + piece.value + " " +
                        piece.year + " " + piece.status);
                }
            }
           
        }

        public static void sellPiece() {
            // ask for the id of the product
            Console.WriteLine("please Enter the ID of the item: ");
            string tempID_1 = Console.ReadLine();
            if (pieceValidation(tempID_1) == true)
            {
                Console.WriteLine("How much do you want to pay?");
                double price = Convert.ToDouble(Console.ReadLine());
                if (price >= returnValue(tempID_1))
                {
                    Console.WriteLine("Item has been sold");
                    UpdateStatus(tempID_1);
                }
                else
                {
                    Console.WriteLine("The value of the requested artpiece is above your suggestion!");
                }
            }
            else
            {
                Console.WriteLine("Item does not exist");
            }
         
        }
        /*public static bool pieceValidation(string id) {
            bool flag = false;
            foreach (artPieces piece in MyArtPieces) {
                if (piece.artId == id)
                    flag = true;
            }
            return flag;
        }*/
        // for(START POINT; TERMINATION CONDITION; STEP)
        // measn start from START POINT, go ahead with step sixe up to reach to  the TERMINSTION CONDITION

        // for(int j=5; j<artPieces.Length; j=j+2)
        //{
        // some codes
        //          }

        // j=5, 7, 9, 11, 13, 15, 17, 19
        // first record: MyArtPieces[0]
        //second record: MyArtPieces[1]
        // third record: MyArtPieces[2]
        public static double returnValue(string id) {
            double value = 0;
            for (int i = 0; i < MyArtPieces.Length; i++) {
                if (MyArtPieces[i].artId == id) {
                    value = MyArtPieces[i].value;
                }
            }
            return value;
        }
   
        public static void changeStatus(string id) {
            for (int j = 0; j < MyArtPieces.Length; j++) {
                if (MyArtPieces[j].artId == id) 
                {
                    MyArtPieces[j].status = 'D';
                }
            }
            
        }
        // Instantiation: Creating a copy from a class or a struct
        // [NAME OF STRUCT] [NAME OF OBJECT]
        // artPieces piece

        // [NAME OF STRUCT] [RECORD] [in] [NAME OF ARRAY]
        // artPieces piece in MyArtPieces

        // artPieces[] MyArtPieces = new MyArtPieces[10];
        // string[] str = new string[10];

        // foreach(artPieces piece in MyArtPieces)
        // means: for every record with name of piece (I have named it piece, it can be anything) saved as a record into array of MyArtPieces which has the format of struct artPieces ....

        // foreach(France OAK in SD_YUL)

            // a==b
            //a=b
            //a=30;
        public static bool pieceValidation(string id)
        {
            bool flag = false;
            foreach(artPieces piece in MyArtPieces)
            {
                if (piece.artId == id) //means if artID field of record piece is equivalent to id
                    flag = true; // then assign true to the flag

            }
            return flag;
        }

        public static void Entry()
        {
            Console.WriteLine("These are the fields of myartist:");
            foreach(Artist art in myArtists)
            {
                Console.WriteLine(art.artistFirstName + " " + art.artistLastName + " " + art.artistID + " " + art.refCurator);
            }
            Console.WriteLine("These are the fields of curator");
                foreach(Curator cur in myCuratorList)
            {
                Console.WriteLine(cur.commission + " " + cur.curatorID + " " + cur.firstName + " " + cur.lastName);
            }
        }
        public static void sellArtPiece()
        {
            Console.WriteLine("Please enter the ID of the artpiece you wish to buy");
            string temp_ID = Console.ReadLine();
            if (pieceValidation(temp_ID) == true)
            {
                Console.WriteLine("how much would you like to pay?");
                double price = Convert.ToDouble(Console.ReadLine());
                if (price >= returnValue(temp_ID))
                {
                    Console.WriteLine("this item has been sold");
                }
                else
                {
                    Console.WriteLine("The price you have written is above the asking price");
                } 
            }
            else
            {
                Console.WriteLine("The item does not exist");
            }
          }       
        }

        public static void statusChange(string id)
        {
            for (int i = 0; i < myArtists.Length; i++)
            {
                if (myArtists[i].artistID == id)
                {
                    myArtists[i].status = 's';
                }
                    
            }
        }
        
        // check for the validity of the item
        // ask for the values
        // if his estimated is greater than or equal to value, sell it and show a message, then update status from "D" to "S"
        // otherwise show error and done.

        // You hav ean array of 100 records, its name is MyArtPieces
        // Fetch record 23
        //Myartpieces[23]
        /*
     * for (initial state; terminatin condition; step){
     * for (int k=0; k<10; k++){
     * console.writeline(hi);
     * console.writeline(hi);
     * }
     * 
     * }
     */
        //changeStatus(13266)
        // id = 13266
    }


}
