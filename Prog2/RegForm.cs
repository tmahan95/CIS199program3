//Timothy Mahan
//CIS 199-01
//Due: 4/5/16
//Program3
/*This program takes the number of credit hours a person is currently taking and decides what year they are. The program further examines
  a users credit hours to determing what day they should be able to register. Also, once the year is determined, the users last name is checked again
  an array to find the specific time they should register on a given day. The day and time are then comined and displyed in a label on the form*/

// Mostly By: Andrew L. Wright
//I left your name so you wouldn't think I was trying to take credit for you work... again. If you want what I wrote I can turn it in, this is just what I gathered you wanted from last class.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prog2
{
    public partial class RegForm : Form
    {
        public RegForm()
        {
            InitializeComponent();
        }

        private void findRegTimeBtn_Click(object sender, EventArgs e)
        {
            const float SENIOR_HOURS = 90;    // Min hours for Senior
            const float JUNIOR_HOURS = 60;    // Min hours for Junior
            const float SOPHOMORE_HOURS = 30; // Min hours for Soph.

            const string DAY1 = "March 30";  // 1st day of registration
            const string DAY2 = "March 31";  // 2nd day of registration
            const string DAY3 = "April 1";   // 3rd day of registration
            const string DAY4 = "April 4";   // 4th day of registration
            const string DAY5 = "April 5";   // 5th day of registration
            const string DAY6 = "April 6";   // 6th day of registration

            char[] lastfs = { 'B', 'D', 'F', 'I', 'L', 'O', 'Q', 'S', 'V', 'Z' };//char array for comparing users last name if freshman or sophomore
            char[] lastjs = { 'D', 'I', 'O', 'S', 'Z' };//char array for comparing users last name if junior or senior
            string[] timefs = { "2:00 PM", "4:00 PM", "8:30 AM", "10:00 AM", "11:30 AM", "2:00 PM", "4:00 PM", "8:30 AM", "10:00 AM", "11:30 AM" };//string array for finding registration time
            string[] timejs = { "4:00 PM", "8:30 AM", "10:00 AM", "11:30 AM", "2:00 PM" };//string array for finding registration time

            
            sbyte index = 0;//variable for controlling time loop

            string lastNameStr;       // Entered last name
            char lastNameLetterCh;    // First letter of last name, as char
            string dateStr = "Error"; // Holds date of registration
            string timeStr = "Error"; // Holds time of registration
            float creditHours;        // Entered credit hours

            if (float.TryParse(creditHrTxt.Text, out creditHours) && creditHours >= 0) // Valid hours
            {
                lastNameStr = lastNameTxt.Text;
                if (lastNameStr.Length > 0) // Empty string?
                {
                    lastNameStr = lastNameStr.ToUpper(); // Ensure upper case
                    lastNameLetterCh = lastNameStr[0];   // First char of last name

                    if (char.IsLetter(lastNameLetterCh)) // Is it a letter?
                    {
                        // Juniors and Seniors share same schedule but different days
                        if (creditHours >= JUNIOR_HOURS)
                        {
                            if (creditHours >= SENIOR_HOURS)
                                dateStr = DAY1;
                            else // Must be juniors
                                dateStr = DAY2;

                            while (lastNameLetterCh > lastjs[index] && index < lastjs.Length - 1)//while name is greater than lastjs[index] and index is less than lastjs.length - 1
                            {
                                index++;
                            }
                            timeStr = timejs[index];//set time to timejs[index]
                        }
                        // Sophomores and Freshmen
                        else // Must be soph/fresh
                        {
                            if (creditHours >= SOPHOMORE_HOURS)
                            {
                                // E-Q on one day
                                if ((lastNameLetterCh >= 'E') && // >= E and
                                    (lastNameLetterCh <= 'Q'))   // <= Q
                                    dateStr = DAY3;
                                else // All other letters on next day
                                    dateStr = DAY4;
                            }
                            else // must be freshman
                            {
                                // E-Q on one day
                                if ((lastNameLetterCh >= 'E') && // >= E and
                                    (lastNameLetterCh <= 'Q'))   // <= Q
                                    dateStr = DAY5;
                                else // All other letters on next day
                                    dateStr = DAY6;
                            }

                            while (lastNameLetterCh > lastfs[index] && index < lastfs.Length - 1) //while name is greater than lastfs index value and the index is less than lastfs.length, keep looking.
                            {
                                index++;
                            }
                            timeStr = timefs[index];//set time to timefs[index]
                        }

                        // Output results
                        dateTimeLbl.Text = dateStr + " at " + timeStr;
                    }
                    else // First char not a letter
                        MessageBox.Show("Enter valid last name!");
                }
                else // Empty textbox
                    MessageBox.Show("Enter a last name!");
            }
            else // Can't parse credit hours
                MessageBox.Show("Please enter valid credit hours earned!");
        }
    }
}
