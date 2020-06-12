using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program_mvm.ViewModels
{
    public class StockViewModel: Conductor<Object>
    {
        MainAtt mainAtt;
      
        public StockViewModel()
        {
            
            mainAtt = MainAtt.getInstance();
            initDB();
        }

        //private List<Stock> stocks;
        private List<Stock> stocks;

        public List<Stock> Stocks
        {
            get { return stocks; }
            set { stocks = value; }
        }

        public void initDB()
        {
            //stocks = mainAtt.Mydb.Stocks.ToList();
            stocks = (from s in mainAtt.Mydb.Stocks join p in mainAtt.Mydb.Products on s.ProductID equals p.ProductID select s).ToList();

        }
        public void DoRestockProduct(string productID, int quantity, double price, bool isRewardProduct)
        {
            string laststockID = "";
            bool toCheckDate = false;
            Stock sTemp = new Stock();
            int lastQuantity=0;
            if (stocks == null||stocks.Count==0)
            {
                laststockID = "ST001";
                sTemp = new Stock();
            }
            else
            {

                sTemp = mainAtt.Mydb.Stocks.Where(a=> a.ProductID.Equals(productID)).OrderByDescending(t=> t.Date).FirstOrDefault();
                DateTime dtTemp= Convert.ToDateTime("1/1/0001");
                System.Windows.MessageBox.Show(productID.ToString());
                if (sTemp != null)
                {
                    dtTemp = Convert.ToDateTime(sTemp.Date).Date;
                    //    System.Windows.MessageBox.Show(sTemp.Date.ToString());
                    //    System.Windows.MessageBox.Show("helloo "+ dtTemp.Date.ToString().Equals(DateTime.Now.Date.ToString()).ToString());
                    //    System.Windows.MessageBox.Show(DateTime.Now.Date.ToString());
                    //    System.Windows.MessageBox.Show(dtTemp.Date.ToString());

                    //
                }


                //THIS IS ONLY FOR DEBUGGING
                //if (sTemp != null)
                //{
                //    System.Windows.MessageBox.Show(DateTime.Now.Date.ToString());

                //    System.Windows.MessageBox.Show(dtTemp.Date.ToString() );

                //    System.Windows.MessageBox.Show((dtTemp.Date.ToString() == (DateTime.Now.Date.ToString())).ToString());
                //}



                if(sTemp!=null && dtTemp.Date.ToString() == (DateTime.Now.Date.ToString()))
                {
                    laststockID = sTemp.StockID.ToString();
                    toCheckDate = true;
                }
                else
                {
                    if(sTemp != null)
                    {
                        lastQuantity = (int)sTemp.AvailableStocks;
                    }
                    laststockID = (stocks[stocks.Count - 1]).StockID.ToString();
                    int lastID = Convert.ToInt32(laststockID[2].ToString() + laststockID[3].ToString() + laststockID[4].ToString()) + 1;
                    int temp = lastID;
                    int count = 0;
                    while (temp != 0)
                    {
                        temp /= 10;
                        count++;
                    }

                    if (count == 1)
                    {
                        laststockID = "ST00" + lastID.ToString();

                    }
                    else if (count == 2)
                    {
                        laststockID = "ST0" + lastID.ToString();
                    }
                    else
                    {
                        laststockID = "ST" + lastID.ToString();
                    }
                }

            }
            if(isRewardProduct == false)
            {
                if (toCheckDate==false)
                {
                    sTemp= new Stock();
                    System.Windows.MessageBox.Show("Test");
                    sTemp.StockID = laststockID;
                    System.Windows.MessageBox.Show(sTemp.StockID);
                    sTemp.ManyIn = 0;
                    sTemp.AvailableStocks = 0;
                    sTemp.Date = DateTime.Now;
                }
                Product s2 = mainAtt.Mydb.Products.Where(p => p.ProductID == productID).FirstOrDefault();

                sTemp.ManyIn += quantity + lastQuantity ;
                double tempStock = (double)sTemp.AvailableStocks * (double)s2.ProductPrice;
                tempStock += quantity * price;

                sTemp.AvailableStocks += quantity;

                tempStock /= (int)sTemp.AvailableStocks;

                System.Windows.MessageBox.Show(sTemp.AvailableStocks.ToString() + " Available stocks " + " Quantity: " + quantity);
                sTemp.ProductID = productID;
                 
                sTemp.IsRewardItem =0;
                // increment the stock with manyIn -- so is the  -->=    Total of Stock
                Console.WriteLine(sTemp.ToString());
                if(toCheckDate == false) 
                    mainAtt.Mydb.Stocks.Add(sTemp);

               
                s2.Price = (decimal)((double)tempStock + (double)tempStock * 0.1);
                
                mainAtt.Mydb.SaveChanges();
                initDB();
            }
            

        }


        public void DoDecreaseStock(List<DetailTransaction> dtList)
        {
            for(int i=0; i<dtList.Count; i++)
            {
                //System.Windows.MessageBox.Show(dtList[i].ProductID);
                //var temp200= mainAtt.Mydb.Stocks.ToList();
                //System.Windows.MessageBox.Show(temp200[1].ProductID);

                string indexProductID = dtList[i].ProductID; 
                var temp100 = mainAtt.Mydb.Stocks.Where(a => indexProductID.Equals(a.ProductID)).OrderByDescending(a=> a.StockID).FirstOrDefault();
                //System.Windows.MessageBox.Show(temp100.ProductID.ToString
                Stock temp = temp100;

                
                DateTime tempDate = ((DateTime)(temp.Date)).Date;
                string laststockID ="";
                DateTime currDate = DateTime.Now.Date;
                System.Windows.MessageBox.Show(tempDate.ToString());
                System.Windows.MessageBox.Show(currDate.ToString());
                System.Windows.MessageBox.Show((currDate.ToString().Equals(tempDate.ToString())).ToString());
                if(currDate.ToString() == tempDate.ToString())
                {
                    temp.ManyOut += dtList[i].quantity;
                    temp.AvailableStocks -= dtList[i].quantity;
                }
                else
                {
                    System.Windows.MessageBox.Show("Test");
                    laststockID = (stocks[stocks.Count - 1]).StockID.ToString();
                    int lastID = Convert.ToInt32(laststockID[2].ToString() + laststockID[3].ToString() + laststockID[4].ToString()) + 1;
                    int temp2 = lastID;
                    int count = 0;
                    while (temp2 != 0)
                    {
                        temp2 /= 10;
                        count++;
                    }

                    if (count == 1)
                    {
                        laststockID = "ST00" + lastID.ToString();

                    }
                    else if (count == 2)
                    {
                        laststockID = "ST0" + lastID.ToString();
                    }
                    else
                    {
                        laststockID = "ST" + lastID.ToString();
                    }
                    Stock s = new Stock();

                    s.AvailableStocks = temp.AvailableStocks - dtList[i].quantity;
                    s.ManyIn = 0;
                    s.RewardStocks = 0;
                    s.BrokenStocks = 0;
                    s.IsRewardItem = 0;
                    s.StockID = laststockID;
                    s.ProductID = dtList[i].ProductID;
                    s.ManyOut = dtList[i].quantity;
                    s.Date = currDate;
                    mainAtt.Mydb.Stocks.Add(s);
                }
                

                    mainAtt.Mydb.SaveChanges();
            }
            initDB();

        }
        

    }
}
