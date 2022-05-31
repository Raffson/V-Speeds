using System.Reflection;
using V_Speeds.Model.Aircrafts;

namespace V_Speeds
{
    public partial class Form1
    {   
        // keep track of the last loaded assembly for external aircraft
        private Assembly? extDLL = null;

        // keep track of the last selected type from extDLL
        private Type? extType = null;


        private Type? SelectExternalAircraft(List<Type?> validTypes)
        {
            MessageBox.Show("Multiple aircraft were found in the supplied DLL file.\nPlease choose a specific aircraft in the next pop-up screen...",
                "Multiple aircraft found!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            Form prompt = new()
            {
                Text = "Select an aircraft...",
                Width = 400,
                Height = 200
            };

            ComboBox inputBox = new()
            {
                Left = 50,
                Top = 50,
                DataSource = validTypes,
                Width = 300,
            };

            Button confirmation = new() { Text = "Continue", Left = 50, Top = 100, AutoSize = true, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };

            Button cancel = new() { Text = "Cancel", Left = 200, Top = 100, AutoSize = true, DialogResult = DialogResult.Cancel };
            cancel.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(cancel);
            prompt.Controls.Add(inputBox);
            var result = prompt.ShowDialog();
            if (result == DialogResult.OK) return (inputBox.SelectedItem as Type);
            else if (result == DialogResult.Cancel) return vcalc.Craft.GetType();
            else return null;
        }

        private void LoadExternalDLL()
        {
            OpenFileDialog ofd = new()
            {
                InitialDirectory = Environment.CurrentDirectory,
                Filter = "DLL files (*.dll)|*.dll",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (ofd.ShowDialog() == DialogResult.OK && ofd.FileName is string fname && fname != string.Empty)
            {
                // load assembly and check if it's a valid DLL...
                extDLL = Assembly.LoadFrom(fname);
                var validTypes = new List<Type?>();
                foreach (var type in extDLL.GetTypes())
                    if (type.BaseType == typeof(Aircraft) || type.BaseType == typeof(AircraftAB))
                        validTypes.Add(type);

                if (validTypes.Count > 1) // choose one...
                    extType = SelectExternalAircraft(validTypes);
                else if (validTypes.Count == 1)
                    extType = validTypes.First();

                if (extType is not null)
                {
                    if (extType != vcalc.Craft.GetType())
                    {
                        object? instance = Activator.CreateInstance(extType);
                        if (instance is Aircraft ac)
                        {
                            vcalc.Craft = ac;
                            dllName.Text = extType.Name;
                            dllName.Visible = true;
                        }
                        else apSelect.SelectedIndex = lastProfileIndex; // FUBAR in this case, revert to last selected index...
                    }
                    else apSelect.SelectedIndex = lastProfileIndex; // cancelled in SelectExternalAircraft, revert to last selected index...
                }
                else
                {
                    MessageBox.Show("No valid aircraft were found in the specified DLL.\nCancelling operation!",
                        "No aircraft found!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    apSelect.SelectedIndex = lastProfileIndex;  // no valid type was found, revert to last selected index...
                }
            }
            else apSelect.SelectedIndex = lastProfileIndex;  // cancelled FileDialog, revert to last selected index...
        }
    }
}
