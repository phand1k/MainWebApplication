namespace MainWebApplication.Areas.Identity.Data
{
    public static class IndividualNumberPerson
    {
        public static int MalePerson(string individualNumber)
        {
            if (individualNumber[6] == '3' || individualNumber[6] == '5')
            {
                return 1;
            }
            else if (individualNumber[6] == '4' || individualNumber[6] == '6')
            {
                return 2;
            }
            return 0;
        }
        public static DateTime BornDatePerson(string individualNumber)
        {
            string individualNumberYear = individualNumber.Substring(0, 2);
            string year = "";
            string month = individualNumber.Substring(2, 2);
            string day = individualNumber.Substring(4, 2);

            if (individualNumber[6] == '3' || individualNumber[6] == '5')
            {
                year = "19" + individualNumberYear;
            }
            else if (individualNumber[6] == '4' || individualNumber[6] == '6')
            {
                year = "20" + individualNumberYear;
            }
            string borndate = day + "." + month + "." + year;
            DateTime dateOfBornPerson = DateTime.ParseExact(borndate, "dd.MM.yyyy",
                                                  System.Globalization.CultureInfo.InvariantCulture);
            return dateOfBornPerson;
        }
    }
}
