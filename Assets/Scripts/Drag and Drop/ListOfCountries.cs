using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfCountries : MonoBehaviour
{
     string afrika= "Afrika: Algerien, Angola, Benin, Botswana, Burkina Faso, Burundi, Kamerun, Kap Verde, Zentralafrikanische Republik, Tschad, Komoren, Kongo, Elfenbeink�ste, Demokratische Republik Kongo, Dschibuti, �gypten, �quatorialguinea, Eritrea, �thiopien, Gabun, Gambia, Ghana, Guinea, Guinea-Bissau, Kenia, Lesotho, Liberia, Libyen, Madagaskar, Malawi, Mali, Mauretanien, Mauritius, Marokko, Mosambik, Namibia, Niger, Nigeria, Ruanda, Sao Tome und Principe, Senegal, Seychellen, Sierra Leone, Somalia, S�dafrika, S�dsudan, Sudan, Swasiland, Togo, Tunesien, Uganda, Vereinigte Republik Tansania, Sambia, Simbabwe.";
     string asien= "Asien: Afghanistan, Armenien, Aserbaidschan, Bahrain, Bangladesch, Bhutan, Brunei Darussalam, Kambodscha, China, Hongkong SAR, Macao SAR, Zypern, Georgien, Indien, Indonesien, Islamische Republik Iran, Irak, Israel, Japan, Jordanien, Kasachstan, Kuwait, Kirgisistan, Demokratische Volksrepublik Laos, Libanon, Malaysia, Malediven, Mongolei, Myanmar, Nepal, Oman, Pakistan, Philippinen, Besetztes Pal�stinensisches Gebiet, Katar, Republik Korea, Saudi-Arabien, Singapur, Sri Lanka, Syrische Arabische Republik, Tadschikistan, Thailand, Timor-Leste, T�rkei, Turkmenistan, Vereinigte Arabische Emirate, Usbekistan, Vietnam, Jemen." +
        "\n\n Ozeanien: Australien, Fidschi, Kiribati, F�derierte Staaten von Mikronesien, Neuseeland, Palau, Papua-Neuguinea, Samoa, Salomonen, Tonga, Vanuatu.";
     string europa= "Europa: Albanien, �sterreich, Wei�russland, Belgien, Bosnien und Herzegowina, Bulgarien, Kroatien, Tschechische Republik, D�nemark, Estland, Finnland, Frankreich, Deutschland, Griechenland, Ungarn, Island, Irland, Italien, Lettland, Litauen, Luxemburg, Malta, Montenegro, Republik Moldau, Nordmazedonien, Niederlande, Norwegen, Polen, Portugal, Rum�nien, Russische F�deration, Serbien (und Kosovo), Slowakei, Slowenien, Spanien, Schweden, Schweiz, Ukraine, Vereinigtes K�nigreich.";
     string s�damerika = "S�damerika: Argentinien, Aruba, Antigua und Barbuda, Bahamas, Belize, Bolivien, Brasilien, Kaimaninseln, Chile, Kolumbien, Costa Rica, Kuba, Cura�ao, Dominica, Dominikanische Republik, Ecuador, El Salvador, Grenada, Guatemala, Guyana, Haiti, Honduras, Jamaika, Mexiko, Nicaragua, Panama, Paraguay, Peru, St. Kitts und Nevis, St. Lucia, Sint Maarten, St. Vincent und die Grenadinen, Suriname, Trinidad und Tobago, Uruguay, Venezuela.";
     string nordamerika = "Nordamerika: Kanada, Vereinigte Staaten von Amerika.";

    public string content;
    private void Start()
    {
        if
        (transform.parent.GetComponentInParent<Continents>().name == "Afrika")
        {
            content = afrika;
        }
        else if (transform.parent.GetComponentInParent<Continents>().name == "Asien")
        {
            content = asien;
        }
        else if (transform.parent.GetComponentInParent<Continents>().name == "Europa")
        {
            content = europa;
        }
        else if (transform.parent.GetComponentInParent<Continents>().name == "S�damerika")
        {
           // Debug.Log("s�damerika");
            content = s�damerika;
        }
        else
        {
          //  Debug.Log(transform.parent.GetComponentInParent<Continents>().name);
          //  Debug.Log("nordamerika");
            content = nordamerika;
        }
    }
}
