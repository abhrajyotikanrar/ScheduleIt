using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime curr_time = DateTime.Now;

        DateTime time1 = DateTime.Parse("12:00 PM");
        DateTime time2 = DateTime.Parse("04:00 PM");
        DateTime time3 = DateTime.Parse("04:00 AM");

        if ((curr_time.TimeOfDay > time3.TimeOfDay) && (curr_time.TimeOfDay < time1.TimeOfDay))
            lblWish.Text = "Good Morning";
        else if ((curr_time.TimeOfDay > time1.TimeOfDay) && (curr_time.TimeOfDay < time2.TimeOfDay))
            lblWish.Text = "Good Afternoon";
        else
            lblWish.Text = "Good Evening";

        lblWelcomeName.Text = Request.Cookies["Name"].Value;
    }
}