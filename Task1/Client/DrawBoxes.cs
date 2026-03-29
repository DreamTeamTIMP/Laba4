namespace Task1
{
    public static class DrawBoxes
    {
        
        public static void FillComboBox(ComboBox comboBox, string drives)
        {
            comboBox.Items.Clear();
            string[] devices = drives.Split('\n');
            foreach (var drive in devices)
            {
                if (drive != "")
                    comboBox.Items.Add(drive);
            }
        }

        public static void FillListBox(ListBox listBox, ComboBox comboBox)
        {
            listBox.Items.Clear();

            if (comboBox.SelectedItem is null) 
                throw new ArgumentNullException("Не был выбран диск");

            string path = comboBox.SelectedItem.ToString();
            try
            {
                foreach (string dir in Directory.GetFileSystemEntries(path))
                {
                    DirectoryInfo di = new(dir);
                    listBox.Items.Add(di.Name);
                }
            }
            catch (UnauthorizedAccessException)
            {

            }
        }
    }

}
