using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

//using System.Threading.Tasks;
//using System.Windows.Data;
//using System.Windows.Navigation;
//using System.Windows.Documents;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;

// my references

using System.IO;



namespace ADozenRenames
{
    public class fileItems  // for displaying the list of files
    {
        public string  lFile   { get; set; }

        public string  lFolder { get; set; }
    }



    /// <summary>
    /// Let's the user rename a bunch of files.
    /// </summary>
    public partial class MainWindow : Window
    {
        private     List<string>   theFiles;

        private     List<List<string>>   previousNames;

        private     Button[]       renameButton;

        private     TextBox[]      newText;

        const       string         SettingsFile  =  "ADozenRenamesSettings.txt";  // written and read in the same dir as the executable

        SolidColorBrush rowBackgroundDark   =  (SolidColorBrush)  new BrushConverter().ConvertFromString( "#f2f2f2" ); 
        SolidColorBrush rowBackgroundLight  =  (SolidColorBrush)  new BrushConverter().ConvertFromString( "#ffffff" ); 
        
        
        public  MainWindow()
        {
            InitializeComponent();

            // customize the row colours whenever the list changes
            lvFiles.ItemContainerGenerator.StatusChanged  +=  new EventHandler( ListStatusChanged );     


            renameButton  =  new  Button[13];       // array of the rename buttons

            newText       =  new TextBox[13];

            int ii = 1;

            previousNames  =  new List<List<string>>();

            renameButton[ii]  =  butF1;      newText[ii]  =  text1;      ii++;
            renameButton[ii]  =  butF2;      newText[ii]  =  text2;      ii++;
            renameButton[ii]  =  butF3;      newText[ii]  =  text3;      ii++;
            renameButton[ii]  =  butF4;      newText[ii]  =  text4;      ii++;
            renameButton[ii]  =  butF5;      newText[ii]  =  text5;      ii++;
            renameButton[ii]  =  butF6;      newText[ii]  =  text6;      ii++;
            renameButton[ii]  =  butF7;      newText[ii]  =  text7;      ii++;
            renameButton[ii]  =  butF8;      newText[ii]  =  text8;      ii++;
            renameButton[ii]  =  butF9;      newText[ii]  =  text9;      ii++;
            renameButton[ii]  =  butF10;     newText[ii]  =  text10;     ii++;
            renameButton[ii]  =  butF11;     newText[ii]  =  text11;     ii++;
            renameButton[ii]  =  butF12;     newText[ii]  =  text12;     ii++;

            LoadSettings();
        }



        private     void        ADozenRenames_Closing( object sender,  System.ComponentModel.CancelEventArgs e )
        {
            SaveSettings();
        }



        private     void        SaveSettings()
        {
            // overwrites the previous file if it exists

	        using (StreamWriter writer = new StreamWriter( SettingsFile ))
	        {
                writer.WriteLine("settings for ADozenRenames");
                writer.WriteLine();
                
                if (lvFiles.Items.Count > 0)
                {
                    fileItems firstItem  =  (fileItems) lvFiles.Items[0];

                     writer.WriteLine( "lastdirectory:" + firstItem.lFolder);
                }


                if (replaceText.Text != "")  writer.WriteLine( "replacewith:"  + replaceText.Text );

                if ( text1.Text != "")  writer.WriteLine(  "f1:"  +  text1.Text );
                if ( text2.Text != "")  writer.WriteLine(  "f2:"  +  text2.Text );
                if ( text3.Text != "")  writer.WriteLine(  "f3:"  +  text3.Text );
                if ( text4.Text != "")  writer.WriteLine(  "f4:"  +  text4.Text );
                if ( text5.Text != "")  writer.WriteLine(  "f5:"  +  text5.Text );
                if ( text6.Text != "")  writer.WriteLine(  "f6:"  +  text6.Text );
                if ( text7.Text != "")  writer.WriteLine(  "f7:"  +  text7.Text );
                if ( text8.Text != "")  writer.WriteLine(  "f8:"  +  text8.Text );
                if ( text9.Text != "")  writer.WriteLine(  "f9:"  +  text9.Text );
                if (text10.Text != "")  writer.WriteLine( "f10:"  + text10.Text );
                if (text11.Text != "")  writer.WriteLine( "f11:"  + text11.Text );
                if (text12.Text != "")  writer.WriteLine( "f12:"  + text12.Text );

	            writer.WriteLine();
	            writer.WriteLine("end");
	        }
        }



        private     void        LoadSettings()
        {
            // To get the executable's directory, don't use Environment.CurrentDirectory, since it can be changed  by shortcut settings, etc. 
            //
            //  string path  =  System.IO.Path.GetDirectoryName( System.Reflection.Assembly.GetExecutingAssembly().Location );


            if (!File.Exists( SettingsFile ))  { return; }

         
            TextReader tr   = new StreamReader( SettingsFile );   // create reader & open file

            string line, firstword, value;


            while ((line = tr.ReadLine()) != null) 
            {
                firstword = (line.Split(':'))[0].Trim().ToLower();  // the first word, trim whitespace, all lower case

                char[] seperator = new char[1];  seperator[0] = ':';

                value = "";

                try { value = (line.Split( seperator, 2))[1]; }  // the complete text after the = sign
                catch {}

                switch (firstword)
                {
                    case "lastdirectory":                                   break;

                    case "replacewith":   replaceText.Text  =  value;       break;

                    case "f1":      text1.Text    = value;      break;                                                              
                    case "f2":      text2.Text    = value;      break;                                                              
                    case "f3":      text3.Text    = value;      break;                                                              
                    case "f4":      text4.Text    = value;      break;                                                              
                    case "f5":      text5.Text    = value;      break;                                                              
                    case "f6":      text6.Text    = value;      break;                                                              
                    case "f7":      text7.Text    = value;      break;                                                              
                    case "f8":      text8.Text    = value;      break;                                                              
                    case "f9":      text9.Text    = value;      break;                                                              
                    case "f10":     text10.Text   = value;      break;                                                              
                    case "f11":     text11.Text   = value;      break;                                                              
                    case "f12":     text12.Text   = value;      break;
                }
            }

            tr.Close();
        }



        private     void        ClearFileList()
        {
            theFiles.Clear();   lvFiles.Items.Clear();
        }



        private     void        FileList_Drop( object sender,  DragEventArgs flgEvent )
        {
            if ( !flgEvent.Data.GetDataPresent( DataFormats.FileDrop ) )  return;  // didn't drop a file list


            theFiles = null;

            theFiles = (flgEvent.Data.GetData( DataFormats.FileDrop, true ) as string[]).ToList();
            
            if ((null == theFiles) || (!theFiles.Any()))  { return; }  // nothing in the list
 
            lvFiles.Items.Clear(); 
            

            List<string> subfiles  =  new List<string>();
            List<string> subdirs   =  new List<string>();

            foreach (string thisFile  in theFiles)  
            { 
                FileAttributes attr = File.GetAttributes( thisFile );    // get the file attributes for file or directory

                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)  
                {
                    subdirs.Add( thisFile );

                    subfiles.AddRange( FolderDrop( thisFile ) );

                    continue;
                }
            }

            foreach (string subd  in subdirs)  { theFiles.Remove(subd); }  // remove and folders from the file list 

            theFiles.AddRange( subfiles );


            foreach (string thisFile  in theFiles)  
            {
                lvFiles.Items.Add(new fileItems { lFile = Path.GetFileName( thisFile ),  lFolder = Path.GetDirectoryName( thisFile ) });
            }

            ButtonUndoLastRename.IsEnabled  =  false;
            ButtonUndoLastRename.Content  =  "undo last rename";

            previousNames.Clear();

            previousNames.Insert( 0,  new List<string>( theFiles ) );

            Status( lvFiles.Items.Count.ToString() + " files dropped in." );
        }



        private     void                Status( string statusMessage )
        {
            statusText.Text  +=  "\r" + statusMessage;

            statusScroll.ScrollToBottom();
        }



        private     List<string>        FolderDrop( string theFolder )
        {
            List<string>  theseFiles    =  Directory.GetFiles( theFolder ).ToList();

            List<string>  theseFolders  =  Directory.GetDirectories( theFolder ).ToList();

            foreach (string thisFile  in theseFolders)  { theseFiles.AddRange( FolderDrop( thisFile )); } //  ooh!  recursive call!

            return theseFiles;
        }



        // set the row colours in the file list
        private     void                ListStatusChanged( object sender,  EventArgs ea )  
        {
            if (lvFiles.ItemContainerGenerator.Status  !=  System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated)   return;


            for ( int ii = 0;   ii <  lvFiles.Items.Count;   ii++ )
            {
                var lvitem  =  lvFiles.ItemContainerGenerator.ContainerFromIndex(ii) as ListViewItem;

                if (lvitem == null)  continue;

              //if (  ii % 2  ==  0)   lvitem.Background  =  rowBackgroundDark; 
                if (ii/4 % 2  ==  0)   lvitem.Background  =  rowBackgroundDark;  
                else                   lvitem.Background  =  rowBackgroundLight; 
            }
        }



        private     void                SelectAllTextBox( object sender,  RoutedEventArgs rea )
        {
            TextBox  tb  =  sender as TextBox;

            if (tb == null)  return;

            tb.SelectAll();
        }



        protected   override  void      OnPreviewKeyDown( KeyEventArgs args )
        {
            bool replace  =  false;

            if( Keyboard.IsKeyDown( Key.LeftCtrl) || Keyboard.IsKeyDown( Key.RightCtrl ) )  { replace = true; }
        
            if (args.SystemKey == Key.F10)  { RenameFiles( 10, replace );  return; }  // F10 is handled differently than other function keys.  It is a System Key.  

            //case Key.F1:    OpenHelp();   return;


            switch (args.Key)
            {
                case  Key.R:    
                    
                    if( Keyboard.IsKeyDown( Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl) )  { ButtonSuffixLowercase_Click( null, null ); }

                    break;

                case Key.F1 :   RenameFiles(  1, replace );   return;     
                case Key.F2 :   RenameFiles(  2, replace );   return;     
                case Key.F3 :   RenameFiles(  3, replace );   return;     
                case Key.F4 :   RenameFiles(  4, replace );   return;     
                case Key.F5 :   RenameFiles(  5, replace );   return;     
                case Key.F6 :   RenameFiles(  6, replace );   return;     
                case Key.F7 :   RenameFiles(  7, replace );   return;     
                case Key.F8 :   RenameFiles(  8, replace );   return;     
                case Key.F9 :   RenameFiles(  9, replace );   return;     
                case Key.F10:   RenameFiles( 11, replace );   return;     // handled above
                case Key.F11:   RenameFiles( 11, replace );   return;     
                case Key.F12:   RenameFiles( 12, replace );   return;     
            }

            base.OnKeyDown( args );
        }



        private     void                ButtonSuffixLowercase_Click( object sender, RoutedEventArgs e )
        {
            if (theFiles == null  ||  theFiles.Count == 0)  return;

            int  listCounter  =  -1;

            int  changeCounter  =  0;

            foreach (var thisFile  in theFiles)
            {
                listCounter++;

                string suffix  =  Path.GetExtension    (thisFile);
                string thisDir =  Path.GetDirectoryName(thisFile);

                if (suffix == suffix.ToLower())  continue;  // it's already lowercase

                string  newName  =  Path.GetFileNameWithoutExtension( thisFile )  +  suffix.ToLower();
                
                string  newFile  =  thisDir + @"\" + newName;

                System.IO.File.Move( thisFile,  newFile );

                this.lvFiles.Items[ listCounter ]  =  new fileItems { lFile = newName,  lFolder = thisDir };

                theFiles[ listCounter ]  =  newFile;

                changeCounter++;
            }

            if (clearFileList.IsChecked == true)  ClearFileList();

            Status( changeCounter.ToString() + " suffixes changed." );
        }



        private     void                buttonRename_Click( object sender, RoutedEventArgs e )
        {
            if (theFiles == null  ||  theFiles.Count == 0)  return;  // nothing to rename

            int btnIndex = -1;

            if (sender == butF1)   btnIndex = 1;
            if (sender == butF2)   btnIndex = 2;
            if (sender == butF3)   btnIndex = 3;
            if (sender == butF4)   btnIndex = 4;
            if (sender == butF5)   btnIndex = 5;
            if (sender == butF6)   btnIndex = 6;
            if (sender == butF7)   btnIndex = 7;
            if (sender == butF8)   btnIndex = 8;
            if (sender == butF9)   btnIndex = 9;
            if (sender == butF10)  btnIndex = 10;
            if (sender == butF11)  btnIndex = 11;
            if (sender == butF12)  btnIndex = 12;

            if (btnIndex < 1)  return;  // didn't recognize the sender
            
            bool replace  =  false; 
                    
            if( Keyboard.IsKeyDown( Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl) )  replace = true;

            RenameFiles( btnIndex, replace );
        }



        private     void                RenameFiles( int buttonIndex,  bool replace )
        {
            int  changeCounter  =  0;

            if (buttonIndex < 1  ||  buttonIndex > 12)  throw new Exception("the button value must be between 1 and 12.  Was sent " + buttonIndex.ToString() + " instead.");

            if (newText[buttonIndex].Text == "")  return;  // no text to insert

            if (theFiles == null  ||  theFiles.Count == 0)  return;   // no files to rename


            // Use a for loop rather than a foreach in order to change the collection while working.
            // If the collection isn't updated, then it isn't possible to rename the files more than once.

            for ( int listCounter = 0;   listCounter < theFiles.Count;   listCounter++ )
            {
                string  thisFile  =  theFiles[listCounter];

                string  suffix   =  Path.GetExtension    (thisFile);
                string  thisDir  =  Path.GetDirectoryName(thisFile);

                string  newName  =  Path.GetFileNameWithoutExtension( thisFile ) + newText[buttonIndex].Text  +  suffix;
                
                if (replace)  
                { 
                    int result  =  RenameWithReplace( thisFile, buttonIndex, listCounter );  
                    
                    if (result < 0)  break;

                    changeCounter  +=  result;

                    continue; 
                }


                string  newFile  =  Path.GetDirectoryName( thisFile ) + @"\" + newName;

                try
                {
                    System.IO.File.Move( thisFile,  newFile );

                    theFiles[ listCounter ]  =  newFile;

                    changeCounter++;
                }
                catch (Exception exc)
                {
                    string errorMsg  =  "Oops.   \r\rTrying to rename  \r\r  " + Path.GetFileNameWithoutExtension( thisFile ) + "  \r\rto  \r\r  " + newName + "  \r\rcaused an error.  Stopping.   \r\r" + exc.Message;
                                    
                    MessageBox.Show( errorMsg );

                    Status( errorMsg );
                    
                    return;
                }

                lvFiles.Items[ listCounter ]  =  new fileItems { lFile = newName,  lFolder = thisDir };
            }

            if (clearFileList.IsChecked == true)  ClearFileList();

            if (changeCounter > 0)  addNamesToUndoList();

            Status( changeCounter.ToString() + " files renamed." ); 
        }



        // returns number of files renamed,  -1 if there is an error
        private     int                 RenameWithReplace( string thisFile,  int butIndex,  int lCounter )
        {
            string replace  =  replaceText.Text;
       
            if (replace == "")  return 0;

            string  oldName  =  Path.GetFileName( thisFile );
            string  thisDir  =  Path.GetDirectoryName(thisFile);

            int pos  =  oldName.IndexOf( replace );

            if (pos < 0)  return 0;  //  text to replace isn't in this name
            
            string  newName  =  oldName.Substring( 0, pos )  +  newText[butIndex].Text  +  oldName.Substring( pos + replace.Length );

            
            try
            {
                System.IO.File.Move( thisFile,  Path.GetDirectoryName( thisFile ) + @"\" + newName );

                // If the collection isn't updated, then it isn't possible to rename the files more than once.
                theFiles[lCounter]  =   Path.GetDirectoryName( thisFile ) + @"\" + newName;
            }
            catch (Exception exc)
            {
                string errorMsg  =  "Oops.    \r  \rTrying to rename  \r  \r  " + Path.GetFileNameWithoutExtension( thisFile ) + "  \r  \rto  \r  \r  " + newName + "  \r \rcaused an error.   \r \rStopping.   \r \r" + exc.Message;

                MessageBox.Show( errorMsg );

                Status( errorMsg );

                return -1;
            }

            lvFiles.Items[ lCounter ]  =  new fileItems { lFile = newName,  lFolder = thisDir };

            return 1;
        }



        private     bool                addNamesToUndoList()
        {
            previousNames.Insert( 0,  new List<string>( theFiles ) );

            int count  =  previousNames.Count;

            if (count < 2)  return false;

            count--;  // the current list doesn't count as an "undo" possibility

            ButtonUndoLastRename.IsEnabled  =  true;

            ButtonUndoLastRename.Content  =  "undo last rename  (" + count.ToString() + ")";

            return true;
        }



        private     void                ButtonUndoLastRename_Click( object sender,  RoutedEventArgs e )
        {
            int  count  =  previousNames.Count;
            
            if (count < 2)  throw new Exception("can't undo last rename unless with less than 2 items in the previousNames list.");

            previousNames.RemoveAt( 0 );

            List<string> oldNames  =  previousNames[ 0 ];   // just to make the code look cleaner

            int changeCounter  =  0;


            for ( int ii = 0;   ii < theFiles.Count;   ii++ )
            {
                string  thisFile  =  theFiles[ii];

                string  oldName   =  oldNames[ii];

                try
                {
                    System.IO.File.Move( thisFile,  oldName );

                    theFiles[ ii ]  =  oldName;

                    changeCounter++;
                }
                catch (Exception exc)
                {
                    string errorMsg  =  "Oops.   \r\rTrying to rename  \r\r  " + Path.GetFileName( thisFile ) + "  \r\rto  \r\r  " + oldName + "  \r\rcaused an error.  Stopping.   \r\r" + exc.Message;
                                    
                    MessageBox.Show( errorMsg );

                    Status( errorMsg );
                    
                    return;
                }                

                string  newName  =  Path.GetFileName( thisFile );
                string  thisDir  =  Path.GetDirectoryName(thisFile);

                lvFiles.Items[ ii ]  =  new fileItems { lFile = Path.GetFileName( oldName ),  lFolder = thisDir };
            }
            
            count--;

            if (count < 2)  { ButtonUndoLastRename.Content  =  "undo last rename";    ButtonUndoLastRename.IsEnabled  =  false; }
            else            { ButtonUndoLastRename.Content  =  "undo last rename  (" + count.ToString() + ")"; }

            Status( "Undid last rename." );
        }

    }
}
