using DisasterAlleviationFoundation.Models;
using DisasterAlleviationFoundation_PART2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;

namespace DisasterAlleviationFoundation_PART2.Controllers
{
    public class HomeController : Controller
    {
        //This will be used to communicate with the database //
        public static SqlConnection conn = new SqlConnection(@"Data Source=st10097535a.database.windows.net;Initial Catalog=ST10097535;User ID=admin1;Password=Password321");
        //Declaring variables for the user login and also register//
        public static string UserID = "";
        public static string Username = "";
        public static string Password = "";
        public static string Email = "";

        public string DisplayID = "User Id: ";

        //Declaring variables for the Admin login//
        public static string AdminID = "";
        public static string AdminUsername = "";

        //Debugging variables//
        public static string DonatorName;
        public static double Amount;
        public static string Date;


        //Debugging variables 2 //
        public static Double NumberofItems;
        public static string ProductName;
        public static string ProductDescription;
        public static string ProductCategory;


        private int Balance = 0;


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult DebuggingPage()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Home()
        {
            //This is a security measure preventing anyone whos not logged in from accessing other pages//  
            if (Username == "" && UserID == "")
            {
                return View("Login");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Donation()
        {
            //This is a security measure preventing anyone whos not logged in from accessing other pages//  
            if (Username == "" && UserID == "")
            {
                return View("Login");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserProfileModel UPM)
        {
            //This is for when the user wants to register//
            String Hash = UPM.Password;
            conn.Open();
            SqlCommand cmd = new SqlCommand("Insert into USERTABLE(USERNAME,EMAIL,USERPASSWORD)values(@Username,@Email,@password)", conn);
            cmd.Parameters.AddWithValue("@Username", UPM.UserName);
            cmd.Parameters.AddWithValue("@Email", UPM.Email);
            cmd.Parameters.AddWithValue("@password", UPM.Password);
            cmd.ExecuteNonQuery();
            conn.Close();
            //This is when the user has successfully registered//
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserProfileModel UPM3)
        {
            //This is for when the user wants to login in the system//
            conn.Open();
            String Input = UPM3.Password;
            String Query = "Select * From USERTABLE where UID >=0 and EMAIL='" + UPM3.Email + "'and USERPASSWORD='" + Input + "';";
            SqlDataAdapter SDA = new SqlDataAdapter(Query, conn);
            DataTable dtbl1 = new DataTable();
            SDA.Fill(dtbl1);
            if (dtbl1.Rows.Count > 0)
            {
                Email = UPM3.Email;
                SqlCommand CMD = new SqlCommand("select UID FROM USERTABLE where EMAIL='" + Email + "'and USERPASSWORD='" + Input + "';", conn);
                SqlDataReader DT = CMD.ExecuteReader();
                while (DT.Read())
                {
                    UserID = DT.GetValue(0).ToString();
                }
                DT.Close();
                SqlCommand CMD2 = new SqlCommand("select USERNAME FROM USERTABLE where EMAIL='" + Email + "'and USERPASSWORD='" + Input + "';", conn);
                SqlDataReader DT2 = CMD2.ExecuteReader();
                while (DT2.Read())
                {
                    Username = DT2.GetValue(0).ToString();
                }
                DT2.Close();
                conn.Close();
                return View("Home");
            }
            else
            {
                conn.Close();
                return View("ErrorLogin");
            }
        }

        [HttpPost]
        public IActionResult Donation(MonetaryDonationModel MDM)
        {
            //This is for when the user is donating
            if (MDM.IsAnonymous == true)
            {
                //This code caters for if the user chose to be anonymous//
                DonatorName = "Anonymous";
                Amount = MDM.DonationAmount;
                Date = MDM.DonationDate;
                conn.Open();
                SqlCommand cmd = new SqlCommand("Insert into MONETARYDONATIONS(DONATORNAME,DONATIONAMOUNT,DONATIONDATE,UID)values(@Donatorname,@Donationamount,@donationdate,@UserID)", conn);
                cmd.Parameters.AddWithValue("@Donatorname", DonatorName);
                cmd.Parameters.AddWithValue("@Donationamount", MDM.DonationAmount);
                cmd.Parameters.AddWithValue("@donationdate", MDM.DonationDate);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.ExecuteNonQuery();
                
                conn.Close();


                conn.Open();
                //Made update to add to the organisations account balance //
                SqlCommand CMD2 = new SqlCommand("Insert into AccountBalance(Balance)values(@Balance)", conn);
                CMD2.Parameters.AddWithValue("@Balance", MDM.DonationAmount);
                CMD2.ExecuteNonQuery();
                conn.Close();

                return View("DonationSuccess");
            }
            else
            {
                DonatorName = Username;
                conn.Open();
                SqlCommand cmd = new SqlCommand("Insert into MONETARYDONATIONS(DONATORNAME,DONATIONAMOUNT,DONATIONDATE,UID)values(@Donatorname,@Donationamount,@donationdate,@UserID)", conn);
                cmd.Parameters.AddWithValue("@Donatorname", DonatorName);
                cmd.Parameters.AddWithValue("@Donationamount", MDM.DonationAmount);
                cmd.Parameters.AddWithValue("@donationdate", MDM.DonationDate);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.ExecuteNonQuery();
                conn.Close();

                conn.Open();
                //Made update to add to the organisations account balance //
                SqlCommand CMD2 = new SqlCommand("Insert into AccountBalance(Balance)values(@Balance)", conn);
                CMD2.Parameters.AddWithValue("@Balance", MDM.DonationAmount);
                CMD2.ExecuteNonQuery();
                conn.Close();
                return View("DonationSuccess");
            }
        }
        [HttpGet]
        public IActionResult DonationGoods()
        {
            //This is a security measure preventing anyone whos not logged in from accessing other pages//  
            if (Username == "" && UserID == "")
            {
                return View("Login");
            }
            return View();
        }

        [HttpPost]
        public IActionResult DonationGoods(GoodsDonationModel GDM)
        {
            //This where the user will be donating goods to the Organization//
            if (GDM.IsAnonymous == true)
            {
                //This is for when the user wants to be anonymous//
                conn.Open();
                DonatorName = "Anonymous";
                NumberofItems = GDM.numberofItems;
                ProductName = GDM.Goodname;
                ProductDescription = GDM.Gooddescription;


                if (GDM.category == "Perishable")
                {
                    SqlCommand cmd = new SqlCommand("Insert into GOODSDONATION(NUMBEROFITEMS,GOODSNAME,GOODDESCRIPTION,DONATORTYPE,PRODCATEGORY,PRODDATE,UID)values(@Numberofitems,@Goodsname,@Gooddescription,@Donatortype,@ProdCategory,@Proddate,@UserID)", conn);
                    ProductCategory = GDM.category;
                    cmd.Parameters.AddWithValue("@Numberofitems", GDM.numberofItems);
                    cmd.Parameters.AddWithValue("@Goodsname", GDM.Goodname);
                    cmd.Parameters.AddWithValue("@Gooddescription", GDM.Gooddescription);
                    cmd.Parameters.AddWithValue("@Donatortype", DonatorName);
                    cmd.Parameters.AddWithValue("@ProdCategory", GDM.category);
                    cmd.Parameters.AddWithValue("@Proddate", GDM.Date);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else if (GDM.category == "Clothes")
                {
                    ProductCategory = GDM.category;
                    SqlCommand cmd = new SqlCommand("Insert into GOODSDONATION(NUMBEROFITEMS,GOODSNAME,GOODDESCRIPTION,DONATORTYPE,PRODCATEGORY,PRODDATE,UID)values(@Numberofitems,@Goodsname,@Gooddescription,@Donatortype,@ProdCategory,@Proddate,@UserID)", conn);
                    ProductCategory = GDM.category;
                    cmd.Parameters.AddWithValue("@Numberofitems", GDM.numberofItems);
                    cmd.Parameters.AddWithValue("@Goodsname", GDM.Goodname);
                    cmd.Parameters.AddWithValue("@Gooddescription", GDM.Gooddescription);
                    cmd.Parameters.AddWithValue("@Donatortype", DonatorName);
                    cmd.Parameters.AddWithValue("@ProdCategory", GDM.category);
                    cmd.Parameters.AddWithValue("@Proddate", GDM.Date);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else if (GDM.category == "Custom")
                {
                    ProductCategory = GDM.customCategory;
                    SqlCommand cmd = new SqlCommand("Insert into GOODSDONATION(NUMBEROFITEMS,GOODSNAME,GOODDESCRIPTION,DONATORTYPE,PRODCATEGORY,PRODDATE,UID)values(@Numberofitems,@Goodsname,@Gooddescription,@Donatortype,@ProdCategory,@Proddate,@UserID)", conn);
                    ProductCategory = GDM.category;
                    cmd.Parameters.AddWithValue("@Numberofitems", GDM.numberofItems);
                    cmd.Parameters.AddWithValue("@Goodsname", GDM.Goodname);
                    cmd.Parameters.AddWithValue("@Gooddescription", GDM.Gooddescription);
                    cmd.Parameters.AddWithValue("@Donatortype", DonatorName);
                    cmd.Parameters.AddWithValue("@ProdCategory", GDM.customCategory);
                    cmd.Parameters.AddWithValue("@Proddate", GDM.Date);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return View("DonationSuccess");
            }
            else
            {
                //this is when the user doesnt want to be annonimous//
                NumberofItems = GDM.numberofItems;
                ProductName = GDM.Goodname;
                ProductDescription = GDM.Gooddescription;


                conn.Open();
                if (GDM.category == "Perishable")
                {
                    DonatorName = Username;
                    SqlCommand cmd = new SqlCommand("Insert into GOODSDONATION(NUMBEROFITEMS,GOODSNAME,GOODDESCRIPTION,DONATORTYPE,PRODCATEGORY,PRODDATE,UID)values(@Numberofitems,@Goodsname,@Gooddescription,@Donatortype,@ProdCategory,@Proddate,@UserID)", conn);
                    ProductCategory = GDM.category;
                    cmd.Parameters.AddWithValue("@Numberofitems", GDM.numberofItems);
                    cmd.Parameters.AddWithValue("@Goodsname", GDM.Goodname);
                    cmd.Parameters.AddWithValue("@Gooddescription", GDM.Gooddescription);
                    cmd.Parameters.AddWithValue("@Donatortype", DonatorName);
                    cmd.Parameters.AddWithValue("@ProdCategory", GDM.category);
                    cmd.Parameters.AddWithValue("@Proddate", GDM.Date);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else if (GDM.category == "Clothes")
                {
                    DonatorName = Username;
                    SqlCommand cmd = new SqlCommand("Insert into GOODSDONATION(NUMBEROFITEMS,GOODSNAME,GOODDESCRIPTION,DONATORTYPE,PRODCATEGORY,PRODDATE,UID)values(@Numberofitems,@Goodsname,@Gooddescription,@Donatortype,@ProdCategory,@Proddate,@UserID)", conn);
                    ProductCategory = GDM.category;
                    cmd.Parameters.AddWithValue("@Numberofitems", GDM.numberofItems);
                    cmd.Parameters.AddWithValue("@Goodsname", GDM.Goodname);
                    cmd.Parameters.AddWithValue("@Gooddescription", GDM.Gooddescription);
                    cmd.Parameters.AddWithValue("@Donatortype", DonatorName);
                    cmd.Parameters.AddWithValue("@ProdCategory", GDM.category);
                    cmd.Parameters.AddWithValue("@Proddate", GDM.Date);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else if (GDM.category == "Custom")
                {
                    DonatorName = Username;
                    ProductCategory = GDM.customCategory;
                    SqlCommand cmd = new SqlCommand("Insert into GOODSDONATION(NUMBEROFITEMS,GOODSNAME,GOODDESCRIPTION,DONATORTYPE,PRODCATEGORY,PRODDATE,UID)values(@Numberofitems,@Goodsname,@Gooddescription,@Donatortype,@ProdCategory,@Proddate,@UserID)", conn);
                    cmd.Parameters.AddWithValue("@Numberofitems", GDM.numberofItems);
                    cmd.Parameters.AddWithValue("@Goodsname", GDM.Goodname);
                    cmd.Parameters.AddWithValue("@Gooddescription", GDM.Gooddescription);
                    cmd.Parameters.AddWithValue("@Donatortype", DonatorName);
                    cmd.Parameters.AddWithValue("@ProdCategory", ProductCategory);
                    cmd.Parameters.AddWithValue("@Proddate", GDM.Date);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return View("DonationSuccess");
            }
        }

        [HttpGet]
        public IActionResult DisasterPage()
        {
            //This is a security measure preventing anyone whos not logged in from accessing other pages//  
            if (Username == "" && UserID == "")
            {
                return View("Login");
            }
            return View();
        }
        [HttpPost]
        public IActionResult DisasterPage(DisasterModel DM)
        { 
            string isfunded ="false";
            int funds = 0;
            string goods ="None";
            //This is for when the user wants to enter the disaster details//
            conn.Open();
            SqlCommand CMD = new SqlCommand("Insert into DISASTER (DISASTERNAME,DISASTERDESCRIPTION,LOCATION,AIDTYPE,DISASTERBEG,DISASTEREND,UID,FUNDS,ISFunded,Goods)values(@DISASTERNAME,@DISASTERDESCRIPTION,@LOCATION,@AIDTYPE,@DISASTERBEG,@DISASTEREND,@UID,@FUNDS,@ISFunded,@Goods)", conn);
            CMD.Parameters.AddWithValue("@DISASTERNAME", DM.DisasterName);
            CMD.Parameters.AddWithValue("@DISASTERDESCRIPTION", DM.DisasterDescription);
            CMD.Parameters.AddWithValue("@LOCATION", DM.Location);
            CMD.Parameters.AddWithValue("@AIDTYPE", DM.AidType);
            CMD.Parameters.AddWithValue("@DISASTERBEG", DM.StartDate);
            CMD.Parameters.AddWithValue("@DISASTEREND", DM.EndDate);
            CMD.Parameters.AddWithValue("@UID",UserID);
            CMD.Parameters.AddWithValue("@FUNDS", funds);
            CMD.Parameters.AddWithValue("@ISFunded", isfunded);
            CMD.Parameters.AddWithValue("@Goods", goods);
            CMD.ExecuteNonQuery();
            conn.Close();
            return View();
        }
        //This section of the code3 its for administration side of the application
        [HttpGet]
        public IActionResult AdminHome()
        {
            if (AdminUsername == "" && AdminID == "")
            {
                return View("AdminLogin");
            }
            return View();
        }

        public IActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AdminLogin(AdminUserModel AUM)
        {
            //This if forwhen the administrator wants to login into the system//
            string hash = AUM.Password;
            conn.Open();
            String Query = "Select * From ADMINISTRATION where ADMIN_ID >=0 and ADMINUSERNAME='" + AUM.UserName + "'and ADMINEMAIL='" + AUM.Email + "'and ADMINPASS='" + hash + "';";
            SqlDataAdapter SDA = new SqlDataAdapter(Query, conn);
            DataTable dtbl1 = new DataTable();
            SDA.Fill(dtbl1);
            if (dtbl1.Rows.Count > 0)
            {

                SqlCommand CMD = new SqlCommand("select ADMIN_ID FROM ADMINISTRATION  where ADMINEMAIL='" + AUM.Email + "'and ADMINPASS='" + hash + "';", conn);
                SqlDataReader DT = CMD.ExecuteReader();
                while (DT.Read())
                {
                    AdminID = DT.GetValue(0).ToString();
                }
                DT.Close();
                SqlCommand CMD2 = new SqlCommand("select ADMINUSERNAME FROM ADMINISTRATION where ADMINEMAIL='" + AUM.Email + "'and ADMINPASS='" + hash + "';", conn);
                SqlDataReader DT2 = CMD2.ExecuteReader();
                while (DT2.Read())
                {
                    AdminUsername = DT2.GetValue(0).ToString();
                }
                DT2.Close();
                conn.Close();
                return View("AdminHome");
            }
            else
            {
                conn.Close();
                return View("ErrorLogin");
            }

        }

        [HttpGet]
        public IActionResult IncomingDonations()
        {
            //this for administrator to view incoming donations//
            if (AdminUsername == "" && AdminID == "")
            {
                return View("AdminLogin");
            }
            else
            {
                List<MonetaryDonationModel> donations = new List<MonetaryDonationModel>();

                conn.Open();
                SqlCommand dts = new SqlCommand("select DID,DONATORNAME,DONATIONAMOUNT,DONATIONDATE from MONETARYDONATIONS where DID >=0;", conn);
                SqlDataReader rd = dts.ExecuteReader();
                while (rd.Read())
                {
                    MonetaryDonationModel donation = new MonetaryDonationModel
                    {
                        DID = Convert.ToInt32(rd["DID"]),
                        DonatorsName = rd["DONATORNAME"].ToString(),
                        DonationAmount = Convert.ToDouble(rd["DONATIONAMOUNT"]),
                        DonationDate = rd["DONATIONDATE"].ToString()
                    };
                    donations.Add(donation);
                }
                conn.Close();

                return View(donations);
            }
        }

        [HttpGet]
        public IActionResult IncomingGoodsDonation()
        {
            //this for the adminiostrator to view incoming goods donation//
            if (AdminUsername == "" && AdminID == "")
            {
                return View("AdminLogin");
            }
            else
            {
                List<GoodsDonationModel> goods = new List<GoodsDonationModel>();
                conn.Open();
                SqlCommand dts = new SqlCommand("select GID,NUMBEROFITEMS,GOODSNAME,GOODDESCRIPTION,DONATORTYPE,PRODCATEGORY,PRODDATE FROM GOODSDONATION WHERE GID >=0;", conn);
                SqlDataReader rd = dts.ExecuteReader();
                while (rd.Read())
                {
                    GoodsDonationModel good = new GoodsDonationModel
                    {
                        GID = Convert.ToInt32(rd["GID"]),
                        numberofItems = Convert.ToInt32(rd["NUMBEROFITEMS"]),
                        Goodname = rd["GOODSNAME"].ToString(),
                        Gooddescription = rd["GOODDESCRIPTION"].ToString(),
                        DonatorsName = rd["DONATORTYPE"].ToString(),
                        Category = rd["PRODCATEGORY"].ToString(),
                        Date = Convert.ToDateTime(rd["PRODDATE"])
                    };
                    goods.Add(good);
                }
                conn.Close();
                return View(goods);
            }
        }

        [HttpGet]
        public IActionResult IncomingDisasters()
        {
            //this is to cater for the admin to view a list of incoming disaster//
            if (AdminUsername == "" && AdminID == "")
            {
                return View("AdminLogin");
            }
            else
            {
                List<DisasterModel> disasters = new List<DisasterModel>();
                conn.Open();
                SqlCommand dts = new SqlCommand("SELECT DID,DISASTERNAME,DISASTERDESCRIPTION,LOCATION,AIDTYPE,DISASTERBEG,DISASTEREND FROM DISASTER WHERE DID >=0;", conn);
                SqlDataReader rd = dts.ExecuteReader();
                while (rd.Read())
                {
                    DisasterModel disaster = new DisasterModel
                    {
                        DID = Convert.ToInt32(rd["DID"]),
                        DisasterName = rd["DISASTERNAME"].ToString(),
                        DisasterDescription = rd["DISASTERDESCRIPTION"].ToString(),
                        Location = rd["LOCATION"].ToString(),
                        AidType = rd["AIDTYPE"].ToString(),
                        StartDate = Convert.ToDateTime(rd["DISASTERBEG"]),
                        EndDate = Convert.ToDateTime(rd["DISASTEREND"])
                    };
                    disasters.Add(disaster);
                }
                conn.Close();
                return View(disasters);
            }

        }

        [HttpGet]
        public IActionResult Logout()
        {
            //This is to allow the user to logout of the application//
            if (Username != "" && UserID != "")
            {
                Username = "";
                UserID = "";
                return View("Login");
            }
            if (AdminUsername != "" && AdminID != "")
            {
                AdminUsername = "";
                AdminID = "";
                return View("Login");
            }
            return View("Login");

        }

        [HttpGet]
        public IActionResult AllocateDisaster()
        {
            if (AdminUsername == "" && AdminID == "")
            {
                return View("AdminLogin");
            }
            else
            {
                String UserList = "";

                conn.Open();
                string query = "SELECT SUM(Balance) FROM AccountBalance";
                SqlCommand cmd = new SqlCommand(query, conn);

                Balance = Convert.ToInt32(cmd.ExecuteScalar());

                ViewBag.TotalBalance = Balance; // Pass the total balance to the view
                conn.Close();

                conn.Open();
                SqlCommand dts = new SqlCommand("SELECT DID,DISASTERNAME,DISASTERDESCRIPTION,LOCATION,AIDTYPE,DISASTERBEG,DISASTEREND FROM DISASTER WHERE DID >=0 AND FUNDS = 0 AND ISFunded='False';", conn);
                SqlDataReader rd = dts.ExecuteReader();
                while (rd.Read())
                {
                    UserList += $"DID: {rd["DID"]}, Disaster Name: {rd["DISASTERNAME"]}, Description: {rd["DISASTERDESCRIPTION"]}, Location: {rd["LOCATION"]}, Aid Type: {rd["AIDTYPE"]}, Begin Date: {rd["DISASTERBEG"]}, End Date: {rd["DISASTEREND"]}\n\n";
                }
                conn.Close();
                ViewBag.userList = UserList;
                return View();
            }
        }
        [HttpPost] // This is a new HTTP POST action for allocating funds
        public IActionResult AllocateDisaster(DisasterModel dsm)
        {
        
                conn.Open();
                string query = "UPDATE DISASTER SET FUNDS='" + dsm.AllocationAmount + "', ISFunded ='True' where DID ='" + dsm.Disaster_id + "';";
                SqlCommand sqd = new SqlCommand(query, conn);
                sqd.ExecuteNonQuery();
                conn.Close();

                conn.Open();
                string updateVariable = "INSERT INTO AccountBalance (BALANCE) VALUES (@Balance)";
                SqlCommand cmd = new SqlCommand(updateVariable, conn);
                // Corrected parameter name from AllicationAmount to AllocationAmount
                cmd.Parameters.AddWithValue("@Balance", -dsm.AllocationAmount);
                cmd.ExecuteNonQuery();
                conn.Close();
                return View("Allocationsuccess");
           
        }

        [HttpGet]
        public IActionResult AllocateGoods()
        {
            if (AdminUsername == "" && AdminID == "")
            {
                return View("AdminLogin");
            }
            else
            {
                String UserList = "";
                conn.Open();
                SqlCommand dts = new SqlCommand("SELECT DID,DISASTERNAME,DISASTERDESCRIPTION,LOCATION,AIDTYPE,DISASTERBEG,DISASTEREND FROM DISASTER WHERE DID >=0 AND GOODS ='None' AND ISFunded='False';", conn);
                SqlDataReader rd = dts.ExecuteReader();
                while (rd.Read())
                {
                    UserList += $"DID: {rd["DID"]}, Disaster Name: {rd["DISASTERNAME"]}, Description: {rd["DISASTERDESCRIPTION"]}, Location: {rd["LOCATION"]}, Aid Type: {rd["AIDTYPE"]}, Begin Date: {rd["DISASTERBEG"]}, End Date: {rd["DISASTEREND"]}";
                }
                conn.Close();
                ViewBag.userList = UserList;

                // Fetch GOODSNAME from the GOODSDONATION table
                List<string> goodsList = new List<string>();
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT GOODSNAME FROM GOODSDONATION;", conn);
                SqlDataReader goodsReader = cmd.ExecuteReader();
                while (goodsReader.Read())
                {
                    goodsList.Add(goodsReader["GOODSNAME"].ToString());
                }
                conn.Close();

                // Set the goodsList as ViewBag to be used in the View
                ViewBag.GoodsList = goodsList;

                return View();
            }
        }
        [HttpPost]
        public IActionResult AllocateGoods(DisasterModel dsmz)
        {
                conn.Open();
                string query = "UPDATE DISASTER SET GOODS=@GoodType, ISFunded='True' WHERE DID=@DID";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@GoodType", dsmz.GoodType);
                cmd.Parameters.AddWithValue("@DID", dsmz.goodsID); // Assuming DID and goodsID are related

                cmd.ExecuteNonQuery();
                conn.Close();

                return View("Allocationsuccess");
        }
        [HttpGet]
        public IActionResult PurchaseGoods()
        {
            if (AdminUsername == "" && AdminID == "")
            {
                return View("AdminLogin");
            }
            else 
            {
            conn.Open();
            string query = "SELECT SUM(Balance) FROM AccountBalance";
            SqlCommand cmd = new SqlCommand(query, conn);

            Balance = Convert.ToInt32(cmd.ExecuteScalar());

            ViewBag.TotalBalance = Balance; // Pass the total balance to the view
            conn.Close();
            return View();
            }
            
        }

        [HttpPost]
        public IActionResult PurchaseGoods(GoodsDonationModel mdm)
        {
            DonatorName = "DisAllfoun";
            int Items = 1;
            conn.Open();
            SqlCommand cmd = new SqlCommand("Insert into GOODSDONATION(NUMBEROFITEMS,GOODSNAME,GOODDESCRIPTION,DONATORTYPE,PRODCATEGORY,PRODDATE,UID)values(@Numberofitems,@Goodsname,@Gooddescription,@Donatortype,@ProdCategory,@Proddate,@UserID)", conn);
            cmd.Parameters.AddWithValue("@Numberofitems", Items);
            cmd.Parameters.AddWithValue("@Goodsname", mdm.Goodname);
            cmd.Parameters.AddWithValue("@Gooddescription", mdm.Gooddescription);
            cmd.Parameters.AddWithValue("@Donatortype", DonatorName);
            cmd.Parameters.AddWithValue("@ProdCategory", mdm.Category);
            cmd.Parameters.AddWithValue("@Proddate", mdm.Date);
            cmd.Parameters.AddWithValue("@UserID", AdminID);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            string updateVariable = "INSERT INTO AccountBalance (BALANCE) VALUES (@Balance)";
            SqlCommand cmd2 = new SqlCommand(updateVariable, conn);
            //Purchasing the good //
            cmd2.Parameters.AddWithValue("@Balance",-mdm.GoodsPrice);
            cmd2.ExecuteNonQuery();
            conn.Close();

            return View("PurchaseSuccess");
        }

        public IActionResult ErrorLogin()
        {
            return View();
        }

        public IActionResult DonationSucccess()
        {
            return View();
        }
        public IActionResult RegistrationSuccess()
        {
            return View();
        }
        public IActionResult PurchaseSuccess()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}