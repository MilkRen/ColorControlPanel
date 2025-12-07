using Microsoft.Win32;

namespace ColorsControlPanel.Register
{
    /// <summary>
    /// Чтение данных с регистра 
    /// </summary>
    public class RegisterRead
    {
        private const string _registryPath = @"HKEY_CURRENT_USER\Control Panel\Colors";

        public static string ColorRead(string Name)
        {
            // Имя значения, которое нужно прочитать
            string valueName = Name;

            // Чтение значения из реестра
            string hilightValue = (string)Registry.GetValue(_registryPath, valueName, null);

            return hilightValue;
        }

        public static void ColorWrite(string Name, byte[] value)
        {
            string valueName = Name;
            string valueData = $"{value[0]} {value[1]} {value[2]}"; // Значение, которое нужно записать

            // Запись значения в регистр
            Registry.SetValue(_registryPath, valueName, valueData, RegistryValueKind.String);
        }
    }
}
