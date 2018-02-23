using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Assigment1; //imported .DLL file.
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;


namespace Assigment3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {
        
     
        public MainWindow()
        { 
            // The following text box are read only. This prevents the user from typing aything on them.
            //*******************************************************************************************
            InitializeComponent();
            NameBox.IsReadOnly = true;
            LocationBox.IsReadOnly = true;
            FareZoneBox.IsReadOnly = true;
            MileageBox.IsReadOnly = true;
            PicFileBox.IsReadOnly = true;
            textBox.IsReadOnly = true;
            //**********************************************************************************************
        }

        //Open the Dialog window to look for the file
        OpenFileDialog openJson = new OpenFileDialog();
        //StationCollection variable is where the information will be save
        StationCollection Collection = new StationCollection();



        private void Open_Json_btn_Click(object sender, RoutedEventArgs e)
        {

            openJson.Filter = "JSON|*.json"; //Filster to only show .json file
            if(openJson.ShowDialog() == true) //When cancel is press it will work as intended. if cancel is press the following code will not run
            {

                //saves file name path into textbox
                string Location = openJson.FileName;
                textBox.Text = Location;
               
          
                FileStream reader = new FileStream(openJson.FileName, FileMode.Open, FileAccess.Read); // reads the path file

                DataContractJsonSerializer inputSerializer;
                inputSerializer = new DataContractJsonSerializer(typeof(StationCollection));

                Collection = (StationCollection)inputSerializer.ReadObject(reader); //saves the file info into the Station Collection variable

                reader.Close();
              
                foreach(station n in Collection.stationList) //List is being poputated with the .JSON file info
                {

                    CollectionListView.Items.Add(n);
                   
                }

                //Sets the window size to match the content.
                this.SizeToContent = SizeToContent.Width;
                this.SizeToContent = SizeToContent.Height;

            }

        }

        private void ShowBtn_Click(object sender, RoutedEventArgs e)
        {
            int temp;
           

            if (StationIdBox.Text.Length == 0) //Promps a message is the user has not enter anything
            {
                MessageBox.Show("Enter something!");
                return;
            }
            if(!Int32.TryParse(StationIdBox.Text, out temp))  //Promps a message is the user enter letters
            {
                MessageBox.Show("Enter Number Only!");
                return;
            }
            
           if(!CollectionListView.HasItems)  //Promps a message is the user tries to search for a station while the list is empty
            {
                MessageBox.Show("List is Empty!");
                return;
            }

            temp = Convert.ToInt32(StationIdBox.Text); //Saves the ID into a variable
          
            foreach (station n in Collection.stationList)
            {
                if(temp > 125) //If the ID enter is higher than 125 it promps the station not found 
                {
                    MessageBox.Show("Station Not Found!");
                    return;
                }
                
                if (temp == n.Id ) //Searches the list for the ID the user is looking for and show the data in the following TextBox
                {
                    
                    NameBox.Text = n.Name;
                    LocationBox.Text = n.location;
                    FareZoneBox.Text = Convert.ToString(n.FareZone);
                    MileageBox.Text = Convert.ToString(n.MilageToPenn);
                    PicFileBox.Text = n.PicFileName;
                   


                }

            }


        }

    }
}
