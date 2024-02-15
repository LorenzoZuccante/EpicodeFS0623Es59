using System;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace ConcessionariaZuccante
{
    public partial class HtmlPage : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["DBconnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopolaAuto();
                PopolaOptional();
                PopolaGaranzia();
            }
        }

        private void PopolaAuto()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT IdAuto, Modello, PrezzoBase, ImagePath FROM Auto", con);
                SqlDataReader reader = cmd.ExecuteReader();
                ddlAuto.DataSource = reader;
                ddlAuto.DataTextField = "Modello";
                ddlAuto.DataValueField = "IdAuto";
                ddlAuto.DataBind();
                ddlAuto.Items.Insert(0, new ListItem("Seleziona Auto", "0"));
            }
        }

        private void PopolaOptional()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT IdOptional, Descrizione, Prezzo FROM Optional", con);
                SqlDataReader reader = cmd.ExecuteReader();
                cblOptional.DataSource = reader;
                cblOptional.DataTextField = "Descrizione";
                cblOptional.DataValueField = "Prezzo";
                cblOptional.DataBind();
            }
        }

        private void PopolaGaranzia()
        {
            ddlGaranzia.Items.Add(new ListItem("Seleziona anni di garanzia", "0"));
            for (int i = 1; i <= 5; i++)
            {
                ddlGaranzia.Items.Add(new ListItem($"{i} anno{(i > 1 ? "/i" : "")}", (120 * i).ToString()));
            }
        }

        protected void ddlAuto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAuto.SelectedValue != "0")
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT PrezzoBase, ImagePath FROM Auto WHERE IdAuto = @IdAuto", con);
                    cmd.Parameters.AddWithValue("@IdAuto", ddlAuto.SelectedValue);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lblPrezzoBase.Text = $"Prezzo base: {reader["PrezzoBase"].ToString()} EUR";
                        imgAuto.ImageUrl = reader["ImagePath"].ToString();
                    }
                }
            }
        }

        protected void btnCalcola_Click(object sender, EventArgs e)
        {
            decimal prezzoBase = 0;
            decimal totaleOptional = 0;
            decimal costoGaranzia = Convert.ToDecimal(ddlGaranzia.SelectedItem.Value);
            decimal totale = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT PrezzoBase FROM Auto WHERE IdAuto = @IdAuto", con);
                cmd.Parameters.AddWithValue("@IdAuto", ddlAuto.SelectedValue);
                prezzoBase = (decimal)cmd.ExecuteScalar();
            }

            foreach (ListItem item in cblOptional.Items)
            {
                if (item.Selected)
                {
                    totaleOptional += Convert.ToDecimal(item.Value);
                }
            }

            totale = prezzoBase + totaleOptional + costoGaranzia;

            lblRisultato.Text = $"Prezzo di base: {prezzoBase} EUR, Totale Optional: {totaleOptional} EUR, Totale Garanzia: {costoGaranzia} EUR, <strong>Totale Complessivo: {totale} EUR</strong>";
        }
    }
}
