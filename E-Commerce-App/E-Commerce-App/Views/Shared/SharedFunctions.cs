namespace E_Commerce_App.Views.Shared
{
    public static class SharedFunctions
    {
        public static bool isNullOrEmpty(string obj)
        {
            if (obj != null && obj != string.Empty)
            {
                return false;
            }
            return true;
        }
    }
}
