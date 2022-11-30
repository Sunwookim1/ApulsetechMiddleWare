using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;


namespace a211_AutoCabinet.Datas
{
    class CreateConfig
    {
        public static void MainConfig()
        {
            var dataBase = new DataBase
            {
                Address = "localhost",
                Port = 3306,
                Database = "a211_128port",
                UserID = "rfiduser",
                Password = "123456",
            };

            var tableLayoutPanelTagDataView = new TableLayoutPanelTagDataView
            {
                MinColumn = 166,
                MinRow = 146,
                MaxColumn = 498,
                MaxRow = 438,
                PanelColumn = 4,
                PanelRow = 2,
                AntNumFont = "맑은 고딕",
                AntNumFontSize = 16,
                TagCountFont = "맑은 고딕",
                TagCountFontSize = 32
            };

            var device = new Device
            {
                ComPort = " ",
                Baudrate = "115200",
                AntCount = "8",

                Timeout = 3000,
                Interval = 100,
                TxOnTime = 400,
                TxOffTime = 0,

                AutoStart = false
            };

            var antEnable = new AntEnable
            {
                Ant1Enable = true,
                Ant2Enable = true,
                Ant3Enable = true,
                Ant4Enable = true,
                Ant5Enable = true,
                Ant6Enable = true,
                Ant7Enable = true,
                Ant8Enable = true,
                Ant9Enable = true,
                Ant10Enable = true,
                Ant11Enable = true,
                Ant12Enable = true,
                Ant13Enable = true,
                Ant14Enable = true,
                Ant15Enable = true,
                Ant16Enable = true,
                Ant17Enable = true,
                Ant18Enable = true,
                Ant19Enable = true,
                Ant20Enable = true,
                Ant21Enable = true,
                Ant22Enable = true,
                Ant23Enable = true,
                Ant24Enable = true,
                Ant25Enable = true,
                Ant26Enable = true,
                Ant27Enable = true,
                Ant28Enable = true,
                Ant29Enable = true,
                Ant30Enable = true,
                Ant31Enable = true,
                Ant32Enable = true,
                Ant33Enable = true,
                Ant34Enable = true,
                Ant35Enable = true,
                Ant36Enable = true,
                Ant37Enable = true,
                Ant38Enable = true,
                Ant39Enable = true,
                Ant40Enable = true,
                Ant41Enable = true,
                Ant42Enable = true,
                Ant43Enable = true,
                Ant44Enable = true,
                Ant45Enable = true,
                Ant46Enable = true,
                Ant47Enable = true,
                Ant48Enable = true,
                Ant49Enable = true,
                Ant50Enable = true,
                Ant51Enable = true,
                Ant52Enable = true,
                Ant53Enable = true,
                Ant54Enable = true,
                Ant55Enable = true,
                Ant56Enable = true,
                Ant57Enable = true,
                Ant58Enable = true,
                Ant59Enable = true,
                Ant60Enable = true,
                Ant61Enable = true,
                Ant62Enable = true,
                Ant63Enable = true,
                Ant64Enable = true,
                Ant65Enable = true,
                Ant66Enable = true,
                Ant67Enable = true,
                Ant68Enable = true,
                Ant69Enable = true,
                Ant70Enable = true,
                Ant71Enable = true,
                Ant72Enable = true,
                Ant73Enable = true,
                Ant74Enable = true,
                Ant75Enable = true,
                Ant76Enable = true,
                Ant77Enable = true,
                Ant78Enable = true,
                Ant79Enable = true,
                Ant80Enable = true,
                Ant81Enable = true,
                Ant82Enable = true,
                Ant83Enable = true,
                Ant84Enable = true,
                Ant85Enable = true,
                Ant86Enable = true,
                Ant87Enable = true,
                Ant88Enable = true,
                Ant89Enable = true,
                Ant90Enable = true,
                Ant91Enable = true,
                Ant92Enable = true,
                Ant93Enable = true,
                Ant94Enable = true,
                Ant95Enable = true,
                Ant96Enable = true,
                Ant97Enable = true,
                Ant98Enable = true,
                Ant99Enable = true,
                Ant100Enable = true,
                Ant101Enable = true,
                Ant102Enable = true,
                Ant103Enable = true,
                Ant104Enable = true,
                Ant105Enable = true,
                Ant106Enable = true,
                Ant107Enable = true,
                Ant108Enable = true,
                Ant109Enable = true,
                Ant110Enable = true,
                Ant111Enable = true,
                Ant112Enable = true,
                Ant113Enable = true,
                Ant114Enable = true,
                Ant115Enable = true,
                Ant116Enable = true,
                Ant117Enable = true,
                Ant118Enable = true,
                Ant119Enable = true,
                Ant120Enable = true,
                Ant121Enable = true,
                Ant122Enable = true,
                Ant123Enable = true,
                Ant124Enable = true,
                Ant125Enable = true,
                Ant126Enable = true,
                Ant127Enable = true,
                Ant128Enable = true
            };

            var antPower = new AntPower
            {
                Ant1Power = 15,
                Ant2Power = 15,
                Ant3Power = 15,
                Ant4Power = 15,
                Ant5Power = 15,
                Ant6Power = 15,
                Ant7Power = 15,
                Ant8Power = 15,
                Ant9Power = 15,
                Ant10Power = 15,
                Ant11Power = 15,
                Ant12Power = 15,
                Ant13Power = 15,
                Ant14Power = 15,
                Ant15Power = 15,
                Ant16Power = 15,
                Ant17Power = 15,
                Ant18Power = 15,
                Ant19Power = 15,
                Ant20Power = 15,
                Ant21Power = 15,
                Ant22Power = 15,
                Ant23Power = 15,
                Ant24Power = 15,
                Ant25Power = 15,
                Ant26Power = 15,
                Ant27Power = 15,
                Ant28Power = 15,
                Ant29Power = 15,
                Ant30Power = 15,
                Ant31Power = 15,
                Ant32Power = 15,
                Ant33Power = 15,
                Ant34Power = 15,
                Ant35Power = 15,
                Ant36Power = 15,
                Ant37Power = 15,
                Ant38Power = 15,
                Ant39Power = 15,
                Ant40Power = 15,
                Ant41Power = 15,
                Ant42Power = 15,
                Ant43Power = 15,
                Ant44Power = 15,
                Ant45Power = 15,
                Ant46Power = 15,
                Ant47Power = 15,
                Ant48Power = 15,
                Ant49Power = 15,
                Ant50Power = 15,
                Ant51Power = 15,
                Ant52Power = 15,
                Ant53Power = 15,
                Ant54Power = 15,
                Ant55Power = 15,
                Ant56Power = 15,
                Ant57Power = 15,
                Ant58Power = 15,
                Ant59Power = 15,
                Ant60Power = 15,
                Ant61Power = 15,
                Ant62Power = 15,
                Ant63Power = 15,
                Ant64Power = 15,
                Ant65Power = 15,
                Ant66Power = 15,
                Ant67Power = 15,
                Ant68Power = 15,
                Ant69Power = 15,
                Ant70Power = 15,
                Ant71Power = 15,
                Ant72Power = 15,
                Ant73Power = 15,
                Ant74Power = 15,
                Ant75Power = 15,
                Ant76Power = 15,
                Ant77Power = 15,
                Ant78Power = 15,
                Ant79Power = 15,
                Ant80Power = 15,
                Ant81Power = 15,
                Ant82Power = 15,
                Ant83Power = 15,
                Ant84Power = 15,
                Ant85Power = 15,
                Ant86Power = 15,
                Ant87Power = 15,
                Ant88Power = 15,
                Ant89Power = 15,
                Ant90Power = 15,
                Ant91Power = 15,
                Ant92Power = 15,
                Ant93Power = 15,
                Ant94Power = 15,
                Ant95Power = 15,
                Ant96Power = 15,
                Ant97Power = 15,
                Ant98Power = 15,
                Ant99Power = 15,
                Ant100Power = 15,
                Ant101Power = 15,
                Ant102Power = 15,
                Ant103Power = 15,
                Ant104Power = 15,
                Ant105Power = 15,
                Ant106Power = 15,
                Ant107Power = 15,
                Ant108Power = 15,
                Ant109Power = 15,
                Ant110Power = 15,
                Ant111Power = 15,
                Ant112Power = 15,
                Ant113Power = 15,
                Ant114Power = 15,
                Ant115Power = 15,
                Ant116Power = 15,
                Ant117Power = 15,
                Ant118Power = 15,
                Ant119Power = 15,
                Ant120Power = 15,
                Ant121Power = 15,
                Ant122Power = 15,
                Ant123Power = 15,
                Ant124Power = 15,
                Ant125Power = 15,
                Ant126Power = 15,
                Ant127Power = 15,
                Ant128Power = 15
            };

            var antCableInfo = new AntCableInfo
            {
                Ant1CableInfo = " ",
                Ant2CableInfo = " ",
                Ant3CableInfo = " ",
                Ant4CableInfo = " ",
                Ant5CableInfo = " ",
                Ant6CableInfo = " ",
                Ant7CableInfo = " ",
                Ant8CableInfo = " ",
                Ant9CableInfo = " ",
                Ant10CableInfo = " ",
                Ant11CableInfo = " ",
                Ant12CableInfo = " ",
                Ant13CableInfo = " ",
                Ant14CableInfo = " ",
                Ant15CableInfo = " ",
                Ant16CableInfo = " ",
                Ant17CableInfo = " ",
                Ant18CableInfo = " ",
                Ant19CableInfo = " ",
                Ant20CableInfo = " ",
                Ant21CableInfo = " ",
                Ant22CableInfo = " ",
                Ant23CableInfo = " ",
                Ant24CableInfo = " ",
                Ant25CableInfo = " ",
                Ant26CableInfo = " ",
                Ant27CableInfo = " ",
                Ant28CableInfo = " ",
                Ant29CableInfo = " ",
                Ant30CableInfo = " ",
                Ant31CableInfo = " ",
                Ant32CableInfo = " ",
                Ant33CableInfo = " ",
                Ant34CableInfo = " ",
                Ant35CableInfo = " ",
                Ant36CableInfo = " ",
                Ant37CableInfo = " ",
                Ant38CableInfo = " ",
                Ant39CableInfo = " ",
                Ant40CableInfo = " ",
                Ant41CableInfo = " ",
                Ant42CableInfo = " ",
                Ant43CableInfo = " ",
                Ant44CableInfo = " ",
                Ant45CableInfo = " ",
                Ant46CableInfo = " ",
                Ant47CableInfo = " ",
                Ant48CableInfo = " ",
                Ant49CableInfo = " ",
                Ant50CableInfo = " ",
                Ant51CableInfo = " ",
                Ant52CableInfo = " ",
                Ant53CableInfo = " ",
                Ant54CableInfo = " ",
                Ant55CableInfo = " ",
                Ant56CableInfo = " ",
                Ant57CableInfo = " ",
                Ant58CableInfo = " ",
                Ant59CableInfo = " ",
                Ant60CableInfo = " ",
                Ant61CableInfo = " ",
                Ant62CableInfo = " ",
                Ant63CableInfo = " ",
                Ant64CableInfo = " ",
                Ant65CableInfo = " ",
                Ant66CableInfo = " ",
                Ant67CableInfo = " ",
                Ant68CableInfo = " ",
                Ant69CableInfo = " ",
                Ant70CableInfo = " ",
                Ant71CableInfo = " ",
                Ant72CableInfo = " ",
                Ant73CableInfo = " ",
                Ant74CableInfo = " ",
                Ant75CableInfo = " ",
                Ant76CableInfo = " ",
                Ant77CableInfo = " ",
                Ant78CableInfo = " ",
                Ant79CableInfo = " ",
                Ant80CableInfo = " ",
                Ant81CableInfo = " ",
                Ant82CableInfo = " ",
                Ant83CableInfo = " ",
                Ant84CableInfo = " ",
                Ant85CableInfo = " ",
                Ant86CableInfo = " ",
                Ant87CableInfo = " ",
                Ant88CableInfo = " ",
                Ant89CableInfo = " ",
                Ant90CableInfo = " ",
                Ant91CableInfo = " ",
                Ant92CableInfo = " ",
                Ant93CableInfo = " ",
                Ant94CableInfo = " ",
                Ant95CableInfo = " ",
                Ant96CableInfo = " ",
                Ant97CableInfo = " ",
                Ant98CableInfo = " ",
                Ant99CableInfo = " ",
                Ant100CableInfo = " ",
                Ant101CableInfo = " ",
                Ant102CableInfo = " ",
                Ant103CableInfo = " ",
                Ant104CableInfo = " ",
                Ant105CableInfo = " ",
                Ant106CableInfo = " ",
                Ant107CableInfo = " ",
                Ant108CableInfo = " ",
                Ant109CableInfo = " ",
                Ant110CableInfo = " ",
                Ant111CableInfo = " ",
                Ant112CableInfo = " ",
                Ant113CableInfo = " ",
                Ant114CableInfo = " ",
                Ant115CableInfo = " ",
                Ant116CableInfo = " ",
                Ant117CableInfo = " ",
                Ant118CableInfo = " ",
                Ant119CableInfo = " ",
                Ant120CableInfo = " ",
                Ant121CableInfo = " ",
                Ant122CableInfo = " ",
                Ant123CableInfo = " ",
                Ant124CableInfo = " ",
                Ant125CableInfo = " ",
                Ant126CableInfo = " ",
                Ant127CableInfo = " ",
                Ant128CableInfo = " "
            };

            var antAntExtInfo = new AntAntExtInfo
            {
                Ant1AntExtInfo = " ",
                Ant2AntExtInfo = " ",
                Ant3AntExtInfo = " ",
                Ant4AntExtInfo = " ",
                Ant5AntExtInfo = " ",
                Ant6AntExtInfo = " ",
                Ant7AntExtInfo = " ",
                Ant8AntExtInfo = " ",
                Ant9AntExtInfo = " ",
                Ant10AntExtInfo = " ",
                Ant11AntExtInfo = " ",
                Ant12AntExtInfo = " ",
                Ant13AntExtInfo = " ",
                Ant14AntExtInfo = " ",
                Ant15AntExtInfo = " ",
                Ant16AntExtInfo = " ",
                Ant17AntExtInfo = " ",
                Ant18AntExtInfo = " ",
                Ant19AntExtInfo = " ",
                Ant20AntExtInfo = " ",
                Ant21AntExtInfo = " ",
                Ant22AntExtInfo = " ",
                Ant23AntExtInfo = " ",
                Ant24AntExtInfo = " ",
                Ant25AntExtInfo = " ",
                Ant26AntExtInfo = " ",
                Ant27AntExtInfo = " ",
                Ant28AntExtInfo = " ",
                Ant29AntExtInfo = " ",
                Ant30AntExtInfo = " ",
                Ant31AntExtInfo = " ",
                Ant32AntExtInfo = " ",
                Ant33AntExtInfo = " ",
                Ant34AntExtInfo = " ",
                Ant35AntExtInfo = " ",
                Ant36AntExtInfo = " ",
                Ant37AntExtInfo = " ",
                Ant38AntExtInfo = " ",
                Ant39AntExtInfo = " ",
                Ant40AntExtInfo = " ",
                Ant41AntExtInfo = " ",
                Ant42AntExtInfo = " ",
                Ant43AntExtInfo = " ",
                Ant44AntExtInfo = " ",
                Ant45AntExtInfo = " ",
                Ant46AntExtInfo = " ",
                Ant47AntExtInfo = " ",
                Ant48AntExtInfo = " ",
                Ant49AntExtInfo = " ",
                Ant50AntExtInfo = " ",
                Ant51AntExtInfo = " ",
                Ant52AntExtInfo = " ",
                Ant53AntExtInfo = " ",
                Ant54AntExtInfo = " ",
                Ant55AntExtInfo = " ",
                Ant56AntExtInfo = " ",
                Ant57AntExtInfo = " ",
                Ant58AntExtInfo = " ",
                Ant59AntExtInfo = " ",
                Ant60AntExtInfo = " ",
                Ant61AntExtInfo = " ",
                Ant62AntExtInfo = " ",
                Ant63AntExtInfo = " ",
                Ant64AntExtInfo = " ",
                Ant65AntExtInfo = " ",
                Ant66AntExtInfo = " ",
                Ant67AntExtInfo = " ",
                Ant68AntExtInfo = " ",
                Ant69AntExtInfo = " ",
                Ant70AntExtInfo = " ",
                Ant71AntExtInfo = " ",
                Ant72AntExtInfo = " ",
                Ant73AntExtInfo = " ",
                Ant74AntExtInfo = " ",
                Ant75AntExtInfo = " ",
                Ant76AntExtInfo = " ",
                Ant77AntExtInfo = " ",
                Ant78AntExtInfo = " ",
                Ant79AntExtInfo = " ",
                Ant80AntExtInfo = " ",
                Ant81AntExtInfo = " ",
                Ant82AntExtInfo = " ",
                Ant83AntExtInfo = " ",
                Ant84AntExtInfo = " ",
                Ant85AntExtInfo = " ",
                Ant86AntExtInfo = " ",
                Ant87AntExtInfo = " ",
                Ant88AntExtInfo = " ",
                Ant89AntExtInfo = " ",
                Ant90AntExtInfo = " ",
                Ant91AntExtInfo = " ",
                Ant92AntExtInfo = " ",
                Ant93AntExtInfo = " ",
                Ant94AntExtInfo = " ",
                Ant95AntExtInfo = " ",
                Ant96AntExtInfo = " ",
                Ant97AntExtInfo = " ",
                Ant98AntExtInfo = " ",
                Ant99AntExtInfo = " ",
                Ant100AntExtInfo = " ",
                Ant101AntExtInfo = " ",
                Ant102AntExtInfo = " ",
                Ant103AntExtInfo = " ",
                Ant104AntExtInfo = " ",
                Ant105AntExtInfo = " ",
                Ant106AntExtInfo = " ",
                Ant107AntExtInfo = " ",
                Ant108AntExtInfo = " ",
                Ant109AntExtInfo = " ",
                Ant110AntExtInfo = " ",
                Ant111AntExtInfo = " ",
                Ant112AntExtInfo = " ",
                Ant113AntExtInfo = " ",
                Ant114AntExtInfo = " ",
                Ant115AntExtInfo = " ",
                Ant116AntExtInfo = " ",
                Ant117AntExtInfo = " ",
                Ant118AntExtInfo = " ",
                Ant119AntExtInfo = " ",
                Ant120AntExtInfo = " ",
                Ant121AntExtInfo = " ",
                Ant122AntExtInfo = " ",
                Ant123AntExtInfo = " ",
                Ant124AntExtInfo = " ",
                Ant125AntExtInfo = " ",
                Ant126AntExtInfo = " ",
                Ant127AntExtInfo = " ",
                Ant128AntExtInfo = " "
            };

            var antUserPosInfo = new AntUserPosInfo
            {
                Ant1UserPosInfo = " ",
                Ant2UserPosInfo = " ",
                Ant3UserPosInfo = " ",
                Ant4UserPosInfo = " ",
                Ant5UserPosInfo = " ",
                Ant6UserPosInfo = " ",
                Ant7UserPosInfo = " ",
                Ant8UserPosInfo = " ",
                Ant9UserPosInfo = " ",
                Ant10UserPosInfo = " ",
                Ant11UserPosInfo = " ",
                Ant12UserPosInfo = " ",
                Ant13UserPosInfo = " ",
                Ant14UserPosInfo = " ",
                Ant15UserPosInfo = " ",
                Ant16UserPosInfo = " ",
                Ant17UserPosInfo = " ",
                Ant18UserPosInfo = " ",
                Ant19UserPosInfo = " ",
                Ant20UserPosInfo = " ",
                Ant21UserPosInfo = " ",
                Ant22UserPosInfo = " ",
                Ant23UserPosInfo = " ",
                Ant24UserPosInfo = " ",
                Ant25UserPosInfo = " ",
                Ant26UserPosInfo = " ",
                Ant27UserPosInfo = " ",
                Ant28UserPosInfo = " ",
                Ant29UserPosInfo = " ",
                Ant30UserPosInfo = " ",
                Ant31UserPosInfo = " ",
                Ant32UserPosInfo = " ",
                Ant33UserPosInfo = " ",
                Ant34UserPosInfo = " ",
                Ant35UserPosInfo = " ",
                Ant36UserPosInfo = " ",
                Ant37UserPosInfo = " ",
                Ant38UserPosInfo = " ",
                Ant39UserPosInfo = " ",
                Ant40UserPosInfo = " ",
                Ant41UserPosInfo = " ",
                Ant42UserPosInfo = " ",
                Ant43UserPosInfo = " ",
                Ant44UserPosInfo = " ",
                Ant45UserPosInfo = " ",
                Ant46UserPosInfo = " ",
                Ant47UserPosInfo = " ",
                Ant48UserPosInfo = " ",
                Ant49UserPosInfo = " ",
                Ant50UserPosInfo = " ",
                Ant51UserPosInfo = " ",
                Ant52UserPosInfo = " ",
                Ant53UserPosInfo = " ",
                Ant54UserPosInfo = " ",
                Ant55UserPosInfo = " ",
                Ant56UserPosInfo = " ",
                Ant57UserPosInfo = " ",
                Ant58UserPosInfo = " ",
                Ant59UserPosInfo = " ",
                Ant60UserPosInfo = " ",
                Ant61UserPosInfo = " ",
                Ant62UserPosInfo = " ",
                Ant63UserPosInfo = " ",
                Ant64UserPosInfo = " ",
                Ant65UserPosInfo = " ",
                Ant66UserPosInfo = " ",
                Ant67UserPosInfo = " ",
                Ant68UserPosInfo = " ",
                Ant69UserPosInfo = " ",
                Ant70UserPosInfo = " ",
                Ant71UserPosInfo = " ",
                Ant72UserPosInfo = " ",
                Ant73UserPosInfo = " ",
                Ant74UserPosInfo = " ",
                Ant75UserPosInfo = " ",
                Ant76UserPosInfo = " ",
                Ant77UserPosInfo = " ",
                Ant78UserPosInfo = " ",
                Ant79UserPosInfo = " ",
                Ant80UserPosInfo = " ",
                Ant81UserPosInfo = " ",
                Ant82UserPosInfo = " ",
                Ant83UserPosInfo = " ",
                Ant84UserPosInfo = " ",
                Ant85UserPosInfo = " ",
                Ant86UserPosInfo = " ",
                Ant87UserPosInfo = " ",
                Ant88UserPosInfo = " ",
                Ant89UserPosInfo = " ",
                Ant90UserPosInfo = " ",
                Ant91UserPosInfo = " ",
                Ant92UserPosInfo = " ",
                Ant93UserPosInfo = " ",
                Ant94UserPosInfo = " ",
                Ant95UserPosInfo = " ",
                Ant96UserPosInfo = " ",
                Ant97UserPosInfo = " ",
                Ant98UserPosInfo = " ",
                Ant99UserPosInfo = " ",
                Ant100UserPosInfo = " ",
                Ant101UserPosInfo = " ",
                Ant102UserPosInfo = " ",
                Ant103UserPosInfo = " ",
                Ant104UserPosInfo = " ",
                Ant105UserPosInfo = " ",
                Ant106UserPosInfo = " ",
                Ant107UserPosInfo = " ",
                Ant108UserPosInfo = " ",
                Ant109UserPosInfo = " ",
                Ant110UserPosInfo = " ",
                Ant111UserPosInfo = " ",
                Ant112UserPosInfo = " ",
                Ant113UserPosInfo = " ",
                Ant114UserPosInfo = " ",
                Ant115UserPosInfo = " ",
                Ant116UserPosInfo = " ",
                Ant117UserPosInfo = " ",
                Ant118UserPosInfo = " ",
                Ant119UserPosInfo = " ",
                Ant120UserPosInfo = " ",
                Ant121UserPosInfo = " ",
                Ant122UserPosInfo = " ",
                Ant123UserPosInfo = " ",
                Ant124UserPosInfo = " ",
                Ant125UserPosInfo = " ",
                Ant126UserPosInfo = " ",
                Ant127UserPosInfo = " ",
                Ant128UserPosInfo = " "
            };

            var antRemarksInfo = new AntRemarksInfo
            {
                Ant1RemarksInfo = " ",
                Ant2RemarksInfo = " ",
                Ant3RemarksInfo = " ",
                Ant4RemarksInfo = " ",
                Ant5RemarksInfo = " ",
                Ant6RemarksInfo = " ",
                Ant7RemarksInfo = " ",
                Ant8RemarksInfo = " ",
                Ant9RemarksInfo = " ",
                Ant10RemarksInfo = " ",
                Ant11RemarksInfo = " ",
                Ant12RemarksInfo = " ",
                Ant13RemarksInfo = " ",
                Ant14RemarksInfo = " ",
                Ant15RemarksInfo = " ",
                Ant16RemarksInfo = " ",
                Ant17RemarksInfo = " ",
                Ant18RemarksInfo = " ",
                Ant19RemarksInfo = " ",
                Ant20RemarksInfo = " ",
                Ant21RemarksInfo = " ",
                Ant22RemarksInfo = " ",
                Ant23RemarksInfo = " ",
                Ant24RemarksInfo = " ",
                Ant25RemarksInfo = " ",
                Ant26RemarksInfo = " ",
                Ant27RemarksInfo = " ",
                Ant28RemarksInfo = " ",
                Ant29RemarksInfo = " ",
                Ant30RemarksInfo = " ",
                Ant31RemarksInfo = " ",
                Ant32RemarksInfo = " ",
                Ant33RemarksInfo = " ",
                Ant34RemarksInfo = " ",
                Ant35RemarksInfo = " ",
                Ant36RemarksInfo = " ",
                Ant37RemarksInfo = " ",
                Ant38RemarksInfo = " ",
                Ant39RemarksInfo = " ",
                Ant40RemarksInfo = " ",
                Ant41RemarksInfo = " ",
                Ant42RemarksInfo = " ",
                Ant43RemarksInfo = " ",
                Ant44RemarksInfo = " ",
                Ant45RemarksInfo = " ",
                Ant46RemarksInfo = " ",
                Ant47RemarksInfo = " ",
                Ant48RemarksInfo = " ",
                Ant49RemarksInfo = " ",
                Ant50RemarksInfo = " ",
                Ant51RemarksInfo = " ",
                Ant52RemarksInfo = " ",
                Ant53RemarksInfo = " ",
                Ant54RemarksInfo = " ",
                Ant55RemarksInfo = " ",
                Ant56RemarksInfo = " ",
                Ant57RemarksInfo = " ",
                Ant58RemarksInfo = " ",
                Ant59RemarksInfo = " ",
                Ant60RemarksInfo = " ",
                Ant61RemarksInfo = " ",
                Ant62RemarksInfo = " ",
                Ant63RemarksInfo = " ",
                Ant64RemarksInfo = " ",
                Ant65RemarksInfo = " ",
                Ant66RemarksInfo = " ",
                Ant67RemarksInfo = " ",
                Ant68RemarksInfo = " ",
                Ant69RemarksInfo = " ",
                Ant70RemarksInfo = " ",
                Ant71RemarksInfo = " ",
                Ant72RemarksInfo = " ",
                Ant73RemarksInfo = " ",
                Ant74RemarksInfo = " ",
                Ant75RemarksInfo = " ",
                Ant76RemarksInfo = " ",
                Ant77RemarksInfo = " ",
                Ant78RemarksInfo = " ",
                Ant79RemarksInfo = " ",
                Ant80RemarksInfo = " ",
                Ant81RemarksInfo = " ",
                Ant82RemarksInfo = " ",
                Ant83RemarksInfo = " ",
                Ant84RemarksInfo = " ",
                Ant85RemarksInfo = " ",
                Ant86RemarksInfo = " ",
                Ant87RemarksInfo = " ",
                Ant88RemarksInfo = " ",
                Ant89RemarksInfo = " ",
                Ant90RemarksInfo = " ",
                Ant91RemarksInfo = " ",
                Ant92RemarksInfo = " ",
                Ant93RemarksInfo = " ",
                Ant94RemarksInfo = " ",
                Ant95RemarksInfo = " ",
                Ant96RemarksInfo = " ",
                Ant97RemarksInfo = " ",
                Ant98RemarksInfo = " ",
                Ant99RemarksInfo = " ",
                Ant100RemarksInfo = " ",
                Ant101RemarksInfo = " ",
                Ant102RemarksInfo = " ",
                Ant103RemarksInfo = " ",
                Ant104RemarksInfo = " ",
                Ant105RemarksInfo = " ",
                Ant106RemarksInfo = " ",
                Ant107RemarksInfo = " ",
                Ant108RemarksInfo = " ",
                Ant109RemarksInfo = " ",
                Ant110RemarksInfo = " ",
                Ant111RemarksInfo = " ",
                Ant112RemarksInfo = " ",
                Ant113RemarksInfo = " ",
                Ant114RemarksInfo = " ",
                Ant115RemarksInfo = " ",
                Ant116RemarksInfo = " ",
                Ant117RemarksInfo = " ",
                Ant118RemarksInfo = " ",
                Ant119RemarksInfo = " ",
                Ant120RemarksInfo = " ",
                Ant121RemarksInfo = " ",
                Ant122RemarksInfo = " ",
                Ant123RemarksInfo = " ",
                Ant124RemarksInfo = " ",
                Ant125RemarksInfo = " ",
                Ant126RemarksInfo = " ",
                Ant127RemarksInfo = " ",
                Ant128RemarksInfo = " "
            };

            var function = new Function
            {
                AntEnable = antEnable,
                AntPower = antPower,
                AntCableInfo = antCableInfo,
                AntAntExtInfo = antAntExtInfo,
                AntUserPosInfo = antUserPosInfo,
                AntRemarksInfo = antRemarksInfo,
                Device = device,
                TableLayoutPanelTagDataView = tableLayoutPanelTagDataView
            };

            var setting = new Setting
            {
                Database = dataBase,
                Function = function
            };

            var configuration = new Configuration
            {
                Setting = setting
            };

            XMLSerialize(configuration);
        }

        static void XMLSerialize(Configuration Configuration)
        {
            if (!File.Exists("Setting.Config"))
            {
                using (var SettingData = File.Create("Setting.Config"))
                {
                    var xmlSerializer = new XmlSerializer(typeof(Configuration));
                    xmlSerializer.Serialize(SettingData, Configuration);
                }
            }
        }
    }



    public class Configuration
    {
        public Setting Setting { get; set; }

    }

    public class Setting
    {
        public Function Function { get; set; }

        public DataBase Database { get; set; }
    }

    public class Function
    {
        public TableLayoutPanelTagDataView TableLayoutPanelTagDataView { get; set; }

        public Device Device { get; set; }

        public AntEnable AntEnable { get; set; }

        public AntPower AntPower { get; set; }

        public AntCableInfo AntCableInfo { get; set; }
        
        public AntAntExtInfo AntAntExtInfo { get; set; }

        public AntUserPosInfo AntUserPosInfo { get; set; }

        public AntRemarksInfo AntRemarksInfo { get; set; }

    }

    public class DataBase
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
    }

    public class Device
    {
        public string ComPort { get; set; }
        public string Baudrate { get; set; }
        public string AntCount { get; set; }
        public int Timeout { get; set; }
        public int Interval { get; set; }
        public int TxOnTime { get; set; }
        public int TxOffTime { get; set; }
        public bool AutoStart { get; set; }
    }

    public class AntEnable
    {
        public bool Ant1Enable { get; set; }
        public bool Ant2Enable { get; set; }
        public bool Ant3Enable { get; set; }
        public bool Ant4Enable { get; set; }
        public bool Ant5Enable { get; set; }
        public bool Ant6Enable { get; set; }
        public bool Ant7Enable { get; set; }
        public bool Ant8Enable { get; set; }
        public bool Ant9Enable { get; set; }
        public bool Ant10Enable { get; set; }
        public bool Ant11Enable { get; set; }
        public bool Ant12Enable { get; set; }
        public bool Ant13Enable { get; set; }
        public bool Ant14Enable { get; set; }
        public bool Ant15Enable { get; set; }
        public bool Ant16Enable { get; set; }
        public bool Ant17Enable { get; set; }
        public bool Ant18Enable { get; set; }
        public bool Ant19Enable { get; set; }
        public bool Ant20Enable { get; set; }
        public bool Ant21Enable { get; set; }
        public bool Ant22Enable { get; set; }
        public bool Ant23Enable { get; set; }
        public bool Ant24Enable { get; set; }
        public bool Ant25Enable { get; set; }
        public bool Ant26Enable { get; set; }
        public bool Ant27Enable { get; set; }
        public bool Ant28Enable { get; set; }
        public bool Ant29Enable { get; set; }
        public bool Ant30Enable { get; set; }
        public bool Ant31Enable { get; set; }
        public bool Ant32Enable { get; set; }
        public bool Ant33Enable { get; set; }
        public bool Ant34Enable { get; set; }
        public bool Ant35Enable { get; set; }
        public bool Ant36Enable { get; set; }
        public bool Ant37Enable { get; set; }
        public bool Ant38Enable { get; set; }
        public bool Ant39Enable { get; set; }
        public bool Ant40Enable { get; set; }
        public bool Ant41Enable { get; set; }
        public bool Ant42Enable { get; set; }
        public bool Ant43Enable { get; set; }
        public bool Ant44Enable { get; set; }
        public bool Ant45Enable { get; set; }
        public bool Ant46Enable { get; set; }
        public bool Ant47Enable { get; set; }
        public bool Ant48Enable { get; set; }
        public bool Ant49Enable { get; set; }
        public bool Ant50Enable { get; set; }
        public bool Ant51Enable { get; set; }
        public bool Ant52Enable { get; set; }
        public bool Ant53Enable { get; set; }
        public bool Ant54Enable { get; set; }
        public bool Ant55Enable { get; set; }
        public bool Ant56Enable { get; set; }
        public bool Ant57Enable { get; set; }
        public bool Ant58Enable { get; set; }
        public bool Ant59Enable { get; set; }
        public bool Ant60Enable { get; set; }
        public bool Ant61Enable { get; set; }
        public bool Ant62Enable { get; set; }
        public bool Ant63Enable { get; set; }
        public bool Ant64Enable { get; set; }
        public bool Ant65Enable { get; set; }
        public bool Ant66Enable { get; set; }
        public bool Ant67Enable { get; set; }
        public bool Ant68Enable { get; set; }
        public bool Ant69Enable { get; set; }
        public bool Ant70Enable { get; set; }
        public bool Ant71Enable { get; set; }
        public bool Ant72Enable { get; set; }
        public bool Ant73Enable { get; set; }
        public bool Ant74Enable { get; set; }
        public bool Ant75Enable { get; set; }
        public bool Ant76Enable { get; set; }
        public bool Ant77Enable { get; set; }
        public bool Ant78Enable { get; set; }
        public bool Ant79Enable { get; set; }
        public bool Ant80Enable { get; set; }
        public bool Ant81Enable { get; set; }
        public bool Ant82Enable { get; set; }
        public bool Ant83Enable { get; set; }
        public bool Ant84Enable { get; set; }
        public bool Ant85Enable { get; set; }
        public bool Ant86Enable { get; set; }
        public bool Ant87Enable { get; set; }
        public bool Ant88Enable { get; set; }
        public bool Ant89Enable { get; set; }
        public bool Ant90Enable { get; set; }
        public bool Ant91Enable { get; set; }
        public bool Ant92Enable { get; set; }
        public bool Ant93Enable { get; set; }
        public bool Ant94Enable { get; set; }
        public bool Ant95Enable { get; set; }
        public bool Ant96Enable { get; set; }
        public bool Ant97Enable { get; set; }
        public bool Ant98Enable { get; set; }
        public bool Ant99Enable { get; set; }
        public bool Ant100Enable { get; set; }
        public bool Ant101Enable { get; set; }
        public bool Ant102Enable { get; set; }
        public bool Ant103Enable { get; set; }
        public bool Ant104Enable { get; set; }
        public bool Ant105Enable { get; set; }
        public bool Ant106Enable { get; set; }
        public bool Ant107Enable { get; set; }
        public bool Ant108Enable { get; set; }
        public bool Ant109Enable { get; set; }
        public bool Ant110Enable { get; set; }
        public bool Ant111Enable { get; set; }
        public bool Ant112Enable { get; set; }
        public bool Ant113Enable { get; set; }
        public bool Ant114Enable { get; set; }
        public bool Ant115Enable { get; set; }
        public bool Ant116Enable { get; set; }
        public bool Ant117Enable { get; set; }
        public bool Ant118Enable { get; set; }
        public bool Ant119Enable { get; set; }
        public bool Ant120Enable { get; set; }
        public bool Ant121Enable { get; set; }
        public bool Ant122Enable { get; set; }
        public bool Ant123Enable { get; set; }
        public bool Ant124Enable { get; set; }
        public bool Ant125Enable { get; set; }
        public bool Ant126Enable { get; set; }
        public bool Ant127Enable { get; set; }
        public bool Ant128Enable { get; set; }
    }

    public class AntPower
    {
        public int Ant1Power { get; set; }
        public int Ant2Power { get; set; }
        public int Ant3Power { get; set; }
        public int Ant4Power { get; set; }
        public int Ant5Power { get; set; }
        public int Ant6Power { get; set; }
        public int Ant7Power { get; set; }
        public int Ant8Power { get; set; }
        public int Ant9Power { get; set; }
        public int Ant10Power { get; set; }
        public int Ant11Power { get; set; }
        public int Ant12Power { get; set; }
        public int Ant13Power { get; set; }
        public int Ant14Power { get; set; }
        public int Ant15Power { get; set; }
        public int Ant16Power { get; set; }
        public int Ant17Power { get; set; }
        public int Ant18Power { get; set; }
        public int Ant19Power { get; set; }
        public int Ant20Power { get; set; }
        public int Ant21Power { get; set; }
        public int Ant22Power { get; set; }
        public int Ant23Power { get; set; }
        public int Ant24Power { get; set; }
        public int Ant25Power { get; set; }
        public int Ant26Power { get; set; }
        public int Ant27Power { get; set; }
        public int Ant28Power { get; set; }
        public int Ant29Power { get; set; }
        public int Ant30Power { get; set; }
        public int Ant31Power { get; set; }
        public int Ant32Power { get; set; }
        public int Ant33Power { get; set; }
        public int Ant34Power { get; set; }
        public int Ant35Power { get; set; }
        public int Ant36Power { get; set; }
        public int Ant37Power { get; set; }
        public int Ant38Power { get; set; }
        public int Ant39Power { get; set; }
        public int Ant40Power { get; set; }
        public int Ant41Power { get; set; }
        public int Ant42Power { get; set; }
        public int Ant43Power { get; set; }
        public int Ant44Power { get; set; }
        public int Ant45Power { get; set; }
        public int Ant46Power { get; set; }
        public int Ant47Power { get; set; }
        public int Ant48Power { get; set; }
        public int Ant49Power { get; set; }
        public int Ant50Power { get; set; }
        public int Ant51Power { get; set; }
        public int Ant52Power { get; set; }
        public int Ant53Power { get; set; }
        public int Ant54Power { get; set; }
        public int Ant55Power { get; set; }
        public int Ant56Power { get; set; }
        public int Ant57Power { get; set; }
        public int Ant58Power { get; set; }
        public int Ant59Power { get; set; }
        public int Ant60Power { get; set; }
        public int Ant61Power { get; set; }
        public int Ant62Power { get; set; }
        public int Ant63Power { get; set; }
        public int Ant64Power { get; set; }
        public int Ant65Power { get; set; }
        public int Ant66Power { get; set; }
        public int Ant67Power { get; set; }
        public int Ant68Power { get; set; }
        public int Ant69Power { get; set; }
        public int Ant70Power { get; set; }
        public int Ant71Power { get; set; }
        public int Ant72Power { get; set; }
        public int Ant73Power { get; set; }
        public int Ant74Power { get; set; }
        public int Ant75Power { get; set; }
        public int Ant76Power { get; set; }
        public int Ant77Power { get; set; }
        public int Ant78Power { get; set; }
        public int Ant79Power { get; set; }
        public int Ant80Power { get; set; }
        public int Ant81Power { get; set; }
        public int Ant82Power { get; set; }
        public int Ant83Power { get; set; }
        public int Ant84Power { get; set; }
        public int Ant85Power { get; set; }
        public int Ant86Power { get; set; }
        public int Ant87Power { get; set; }
        public int Ant88Power { get; set; }
        public int Ant89Power { get; set; }
        public int Ant90Power { get; set; }
        public int Ant91Power { get; set; }
        public int Ant92Power { get; set; }
        public int Ant93Power { get; set; }
        public int Ant94Power { get; set; }
        public int Ant95Power { get; set; }
        public int Ant96Power { get; set; }
        public int Ant97Power { get; set; }
        public int Ant98Power { get; set; }
        public int Ant99Power { get; set; }
        public int Ant100Power { get; set; }
        public int Ant101Power { get; set; }
        public int Ant102Power { get; set; }
        public int Ant103Power { get; set; }
        public int Ant104Power { get; set; }
        public int Ant105Power { get; set; }
        public int Ant106Power { get; set; }
        public int Ant107Power { get; set; }
        public int Ant108Power { get; set; }
        public int Ant109Power { get; set; }
        public int Ant110Power { get; set; }
        public int Ant111Power { get; set; }
        public int Ant112Power { get; set; }
        public int Ant113Power { get; set; }
        public int Ant114Power { get; set; }
        public int Ant115Power { get; set; }
        public int Ant116Power { get; set; }
        public int Ant117Power { get; set; }
        public int Ant118Power { get; set; }
        public int Ant119Power { get; set; }
        public int Ant120Power { get; set; }
        public int Ant121Power { get; set; }
        public int Ant122Power { get; set; }
        public int Ant123Power { get; set; }
        public int Ant124Power { get; set; }
        public int Ant125Power { get; set; }
        public int Ant126Power { get; set; }
        public int Ant127Power { get; set; }
        public int Ant128Power { get; set; }
    }

    public class AntCableInfo
    {
        public string Ant1CableInfo { get; set; }
        public string Ant2CableInfo { get; set; }
        public string Ant3CableInfo { get; set; }
        public string Ant4CableInfo { get; set; }
        public string Ant5CableInfo { get; set; }
        public string Ant6CableInfo { get; set; }
        public string Ant7CableInfo { get; set; }
        public string Ant8CableInfo { get; set; }
        public string Ant9CableInfo { get; set; }
        public string Ant10CableInfo { get; set; }
        public string Ant11CableInfo { get; set; }
        public string Ant12CableInfo { get; set; }
        public string Ant13CableInfo { get; set; }
        public string Ant14CableInfo { get; set; }
        public string Ant15CableInfo { get; set; }
        public string Ant16CableInfo { get; set; }
        public string Ant17CableInfo { get; set; }
        public string Ant18CableInfo { get; set; }
        public string Ant19CableInfo { get; set; }
        public string Ant20CableInfo { get; set; }
        public string Ant21CableInfo { get; set; }
        public string Ant22CableInfo { get; set; }
        public string Ant23CableInfo { get; set; }
        public string Ant24CableInfo { get; set; }
        public string Ant25CableInfo { get; set; }
        public string Ant26CableInfo { get; set; }
        public string Ant27CableInfo { get; set; }
        public string Ant28CableInfo { get; set; }
        public string Ant29CableInfo { get; set; }
        public string Ant30CableInfo { get; set; }
        public string Ant31CableInfo { get; set; }
        public string Ant32CableInfo { get; set; }
        public string Ant33CableInfo { get; set; }
        public string Ant34CableInfo { get; set; }
        public string Ant35CableInfo { get; set; }
        public string Ant36CableInfo { get; set; }
        public string Ant37CableInfo { get; set; }
        public string Ant38CableInfo { get; set; }
        public string Ant39CableInfo { get; set; }
        public string Ant40CableInfo { get; set; }
        public string Ant41CableInfo { get; set; }
        public string Ant42CableInfo { get; set; }
        public string Ant43CableInfo { get; set; }
        public string Ant44CableInfo { get; set; }
        public string Ant45CableInfo { get; set; }
        public string Ant46CableInfo { get; set; }
        public string Ant47CableInfo { get; set; }
        public string Ant48CableInfo { get; set; }
        public string Ant49CableInfo { get; set; }
        public string Ant50CableInfo { get; set; }
        public string Ant51CableInfo { get; set; }
        public string Ant52CableInfo { get; set; }
        public string Ant53CableInfo { get; set; }
        public string Ant54CableInfo { get; set; }
        public string Ant55CableInfo { get; set; }
        public string Ant56CableInfo { get; set; }
        public string Ant57CableInfo { get; set; }
        public string Ant58CableInfo { get; set; }
        public string Ant59CableInfo { get; set; }
        public string Ant60CableInfo { get; set; }
        public string Ant61CableInfo { get; set; }
        public string Ant62CableInfo { get; set; }
        public string Ant63CableInfo { get; set; }
        public string Ant64CableInfo { get; set; }
        public string Ant65CableInfo { get; set; }
        public string Ant66CableInfo { get; set; }
        public string Ant67CableInfo { get; set; }
        public string Ant68CableInfo { get; set; }
        public string Ant69CableInfo { get; set; }
        public string Ant70CableInfo { get; set; }
        public string Ant71CableInfo { get; set; }
        public string Ant72CableInfo { get; set; }
        public string Ant73CableInfo { get; set; }
        public string Ant74CableInfo { get; set; }
        public string Ant75CableInfo { get; set; }
        public string Ant76CableInfo { get; set; }
        public string Ant77CableInfo { get; set; }
        public string Ant78CableInfo { get; set; }
        public string Ant79CableInfo { get; set; }
        public string Ant80CableInfo { get; set; }
        public string Ant81CableInfo { get; set; }
        public string Ant82CableInfo { get; set; }
        public string Ant83CableInfo { get; set; }
        public string Ant84CableInfo { get; set; }
        public string Ant85CableInfo { get; set; }
        public string Ant86CableInfo { get; set; }
        public string Ant87CableInfo { get; set; }
        public string Ant88CableInfo { get; set; }
        public string Ant89CableInfo { get; set; }
        public string Ant90CableInfo { get; set; }
        public string Ant91CableInfo { get; set; }
        public string Ant92CableInfo { get; set; }
        public string Ant93CableInfo { get; set; }
        public string Ant94CableInfo { get; set; }
        public string Ant95CableInfo { get; set; }
        public string Ant96CableInfo { get; set; }
        public string Ant97CableInfo { get; set; }
        public string Ant98CableInfo { get; set; }
        public string Ant99CableInfo { get; set; }
        public string Ant100CableInfo { get; set; }
        public string Ant101CableInfo { get; set; }
        public string Ant102CableInfo { get; set; }
        public string Ant103CableInfo { get; set; }
        public string Ant104CableInfo { get; set; }
        public string Ant105CableInfo { get; set; }
        public string Ant106CableInfo { get; set; }
        public string Ant107CableInfo { get; set; }
        public string Ant108CableInfo { get; set; }
        public string Ant109CableInfo { get; set; }
        public string Ant110CableInfo { get; set; }
        public string Ant111CableInfo { get; set; }
        public string Ant112CableInfo { get; set; }
        public string Ant113CableInfo { get; set; }
        public string Ant114CableInfo { get; set; }
        public string Ant115CableInfo { get; set; }
        public string Ant116CableInfo { get; set; }
        public string Ant117CableInfo { get; set; }
        public string Ant118CableInfo { get; set; }
        public string Ant119CableInfo { get; set; }
        public string Ant120CableInfo { get; set; }
        public string Ant121CableInfo { get; set; }
        public string Ant122CableInfo { get; set; }
        public string Ant123CableInfo { get; set; }
        public string Ant124CableInfo { get; set; }
        public string Ant125CableInfo { get; set; }
        public string Ant126CableInfo { get; set; }
        public string Ant127CableInfo { get; set; }
        public string Ant128CableInfo { get; set; }
    }

    public class AntAntExtInfo
    {
        public string Ant1AntExtInfo { get; set; }
        public string Ant2AntExtInfo { get; set; }
        public string Ant3AntExtInfo { get; set; }
        public string Ant4AntExtInfo { get; set; }
        public string Ant5AntExtInfo { get; set; }
        public string Ant6AntExtInfo { get; set; }
        public string Ant7AntExtInfo { get; set; }
        public string Ant8AntExtInfo { get; set; }
        public string Ant9AntExtInfo { get; set; }
        public string Ant10AntExtInfo { get; set; }
        public string Ant11AntExtInfo { get; set; }
        public string Ant12AntExtInfo { get; set; }
        public string Ant13AntExtInfo { get; set; }
        public string Ant14AntExtInfo { get; set; }
        public string Ant15AntExtInfo { get; set; }
        public string Ant16AntExtInfo { get; set; }
        public string Ant17AntExtInfo { get; set; }
        public string Ant18AntExtInfo { get; set; }
        public string Ant19AntExtInfo { get; set; }
        public string Ant20AntExtInfo { get; set; }
        public string Ant21AntExtInfo { get; set; }
        public string Ant22AntExtInfo { get; set; }
        public string Ant23AntExtInfo { get; set; }
        public string Ant24AntExtInfo { get; set; }
        public string Ant25AntExtInfo { get; set; }
        public string Ant26AntExtInfo { get; set; }
        public string Ant27AntExtInfo { get; set; }
        public string Ant28AntExtInfo { get; set; }
        public string Ant29AntExtInfo { get; set; }
        public string Ant30AntExtInfo { get; set; }
        public string Ant31AntExtInfo { get; set; }
        public string Ant32AntExtInfo { get; set; }
        public string Ant33AntExtInfo { get; set; }
        public string Ant34AntExtInfo { get; set; }
        public string Ant35AntExtInfo { get; set; }
        public string Ant36AntExtInfo { get; set; }
        public string Ant37AntExtInfo { get; set; }
        public string Ant38AntExtInfo { get; set; }
        public string Ant39AntExtInfo { get; set; }
        public string Ant40AntExtInfo { get; set; }
        public string Ant41AntExtInfo { get; set; }
        public string Ant42AntExtInfo { get; set; }
        public string Ant43AntExtInfo { get; set; }
        public string Ant44AntExtInfo { get; set; }
        public string Ant45AntExtInfo { get; set; }
        public string Ant46AntExtInfo { get; set; }
        public string Ant47AntExtInfo { get; set; }
        public string Ant48AntExtInfo { get; set; }
        public string Ant49AntExtInfo { get; set; }
        public string Ant50AntExtInfo { get; set; }
        public string Ant51AntExtInfo { get; set; }
        public string Ant52AntExtInfo { get; set; }
        public string Ant53AntExtInfo { get; set; }
        public string Ant54AntExtInfo { get; set; }
        public string Ant55AntExtInfo { get; set; }
        public string Ant56AntExtInfo { get; set; }
        public string Ant57AntExtInfo { get; set; }
        public string Ant58AntExtInfo { get; set; }
        public string Ant59AntExtInfo { get; set; }
        public string Ant60AntExtInfo { get; set; }
        public string Ant61AntExtInfo { get; set; }
        public string Ant62AntExtInfo { get; set; }
        public string Ant63AntExtInfo { get; set; }
        public string Ant64AntExtInfo { get; set; }
        public string Ant65AntExtInfo { get; set; }
        public string Ant66AntExtInfo { get; set; }
        public string Ant67AntExtInfo { get; set; }
        public string Ant68AntExtInfo { get; set; }
        public string Ant69AntExtInfo { get; set; }
        public string Ant70AntExtInfo { get; set; }
        public string Ant71AntExtInfo { get; set; }
        public string Ant72AntExtInfo { get; set; }
        public string Ant73AntExtInfo { get; set; }
        public string Ant74AntExtInfo { get; set; }
        public string Ant75AntExtInfo { get; set; }
        public string Ant76AntExtInfo { get; set; }
        public string Ant77AntExtInfo { get; set; }
        public string Ant78AntExtInfo { get; set; }
        public string Ant79AntExtInfo { get; set; }
        public string Ant80AntExtInfo { get; set; }
        public string Ant81AntExtInfo { get; set; }
        public string Ant82AntExtInfo { get; set; }
        public string Ant83AntExtInfo { get; set; }
        public string Ant84AntExtInfo { get; set; }
        public string Ant85AntExtInfo { get; set; }
        public string Ant86AntExtInfo { get; set; }
        public string Ant87AntExtInfo { get; set; }
        public string Ant88AntExtInfo { get; set; }
        public string Ant89AntExtInfo { get; set; }
        public string Ant90AntExtInfo { get; set; }
        public string Ant91AntExtInfo { get; set; }
        public string Ant92AntExtInfo { get; set; }
        public string Ant93AntExtInfo { get; set; }
        public string Ant94AntExtInfo { get; set; }
        public string Ant95AntExtInfo { get; set; }
        public string Ant96AntExtInfo { get; set; }
        public string Ant97AntExtInfo { get; set; }
        public string Ant98AntExtInfo { get; set; }
        public string Ant99AntExtInfo { get; set; }
        public string Ant100AntExtInfo { get; set; }
        public string Ant101AntExtInfo { get; set; }
        public string Ant102AntExtInfo { get; set; }
        public string Ant103AntExtInfo { get; set; }
        public string Ant104AntExtInfo { get; set; }
        public string Ant105AntExtInfo { get; set; }
        public string Ant106AntExtInfo { get; set; }
        public string Ant107AntExtInfo { get; set; }
        public string Ant108AntExtInfo { get; set; }
        public string Ant109AntExtInfo { get; set; }
        public string Ant110AntExtInfo { get; set; }
        public string Ant111AntExtInfo { get; set; }
        public string Ant112AntExtInfo { get; set; }
        public string Ant113AntExtInfo { get; set; }
        public string Ant114AntExtInfo { get; set; }
        public string Ant115AntExtInfo { get; set; }
        public string Ant116AntExtInfo { get; set; }
        public string Ant117AntExtInfo { get; set; }
        public string Ant118AntExtInfo { get; set; }
        public string Ant119AntExtInfo { get; set; }
        public string Ant120AntExtInfo { get; set; }
        public string Ant121AntExtInfo { get; set; }
        public string Ant122AntExtInfo { get; set; }
        public string Ant123AntExtInfo { get; set; }
        public string Ant124AntExtInfo { get; set; }
        public string Ant125AntExtInfo { get; set; }
        public string Ant126AntExtInfo { get; set; }
        public string Ant127AntExtInfo { get; set; }
        public string Ant128AntExtInfo { get; set; }
    }

    public class AntUserPosInfo
    {
        public string Ant1UserPosInfo { get; set; }
        public string Ant2UserPosInfo { get; set; }
        public string Ant3UserPosInfo { get; set; }
        public string Ant4UserPosInfo { get; set; }
        public string Ant5UserPosInfo { get; set; }
        public string Ant6UserPosInfo { get; set; }
        public string Ant7UserPosInfo { get; set; }
        public string Ant8UserPosInfo { get; set; }
        public string Ant9UserPosInfo { get; set; }
        public string Ant10UserPosInfo { get; set; }
        public string Ant11UserPosInfo { get; set; }
        public string Ant12UserPosInfo { get; set; }
        public string Ant13UserPosInfo { get; set; }
        public string Ant14UserPosInfo { get; set; }
        public string Ant15UserPosInfo { get; set; }
        public string Ant16UserPosInfo { get; set; }
        public string Ant17UserPosInfo { get; set; }
        public string Ant18UserPosInfo { get; set; }
        public string Ant19UserPosInfo { get; set; }
        public string Ant20UserPosInfo { get; set; }
        public string Ant21UserPosInfo { get; set; }
        public string Ant22UserPosInfo { get; set; }
        public string Ant23UserPosInfo { get; set; }
        public string Ant24UserPosInfo { get; set; }
        public string Ant25UserPosInfo { get; set; }
        public string Ant26UserPosInfo { get; set; }
        public string Ant27UserPosInfo { get; set; }
        public string Ant28UserPosInfo { get; set; }
        public string Ant29UserPosInfo { get; set; }
        public string Ant30UserPosInfo { get; set; }
        public string Ant31UserPosInfo { get; set; }
        public string Ant32UserPosInfo { get; set; }
        public string Ant33UserPosInfo { get; set; }
        public string Ant34UserPosInfo { get; set; }
        public string Ant35UserPosInfo { get; set; }
        public string Ant36UserPosInfo { get; set; }
        public string Ant37UserPosInfo { get; set; }
        public string Ant38UserPosInfo { get; set; }
        public string Ant39UserPosInfo { get; set; }
        public string Ant40UserPosInfo { get; set; }
        public string Ant41UserPosInfo { get; set; }
        public string Ant42UserPosInfo { get; set; }
        public string Ant43UserPosInfo { get; set; }
        public string Ant44UserPosInfo { get; set; }
        public string Ant45UserPosInfo { get; set; }
        public string Ant46UserPosInfo { get; set; }
        public string Ant47UserPosInfo { get; set; }
        public string Ant48UserPosInfo { get; set; }
        public string Ant49UserPosInfo { get; set; }
        public string Ant50UserPosInfo { get; set; }
        public string Ant51UserPosInfo { get; set; }
        public string Ant52UserPosInfo { get; set; }
        public string Ant53UserPosInfo { get; set; }
        public string Ant54UserPosInfo { get; set; }
        public string Ant55UserPosInfo { get; set; }
        public string Ant56UserPosInfo { get; set; }
        public string Ant57UserPosInfo { get; set; }
        public string Ant58UserPosInfo { get; set; }
        public string Ant59UserPosInfo { get; set; }
        public string Ant60UserPosInfo { get; set; }
        public string Ant61UserPosInfo { get; set; }
        public string Ant62UserPosInfo { get; set; }
        public string Ant63UserPosInfo { get; set; }
        public string Ant64UserPosInfo { get; set; }
        public string Ant65UserPosInfo { get; set; }
        public string Ant66UserPosInfo { get; set; }
        public string Ant67UserPosInfo { get; set; }
        public string Ant68UserPosInfo { get; set; }
        public string Ant69UserPosInfo { get; set; }
        public string Ant70UserPosInfo { get; set; }
        public string Ant71UserPosInfo { get; set; }
        public string Ant72UserPosInfo { get; set; }
        public string Ant73UserPosInfo { get; set; }
        public string Ant74UserPosInfo { get; set; }
        public string Ant75UserPosInfo { get; set; }
        public string Ant76UserPosInfo { get; set; }
        public string Ant77UserPosInfo { get; set; }
        public string Ant78UserPosInfo { get; set; }
        public string Ant79UserPosInfo { get; set; }
        public string Ant80UserPosInfo { get; set; }
        public string Ant81UserPosInfo { get; set; }
        public string Ant82UserPosInfo { get; set; }
        public string Ant83UserPosInfo { get; set; }
        public string Ant84UserPosInfo { get; set; }
        public string Ant85UserPosInfo { get; set; }
        public string Ant86UserPosInfo { get; set; }
        public string Ant87UserPosInfo { get; set; }
        public string Ant88UserPosInfo { get; set; }
        public string Ant89UserPosInfo { get; set; }
        public string Ant90UserPosInfo { get; set; }
        public string Ant91UserPosInfo { get; set; }
        public string Ant92UserPosInfo { get; set; }
        public string Ant93UserPosInfo { get; set; }
        public string Ant94UserPosInfo { get; set; }
        public string Ant95UserPosInfo { get; set; }
        public string Ant96UserPosInfo { get; set; }
        public string Ant97UserPosInfo { get; set; }
        public string Ant98UserPosInfo { get; set; }
        public string Ant99UserPosInfo { get; set; }
        public string Ant100UserPosInfo { get; set; }
        public string Ant101UserPosInfo { get; set; }
        public string Ant102UserPosInfo { get; set; }
        public string Ant103UserPosInfo { get; set; }
        public string Ant104UserPosInfo { get; set; }
        public string Ant105UserPosInfo { get; set; }
        public string Ant106UserPosInfo { get; set; }
        public string Ant107UserPosInfo { get; set; }
        public string Ant108UserPosInfo { get; set; }
        public string Ant109UserPosInfo { get; set; }
        public string Ant110UserPosInfo { get; set; }
        public string Ant111UserPosInfo { get; set; }
        public string Ant112UserPosInfo { get; set; }
        public string Ant113UserPosInfo { get; set; }
        public string Ant114UserPosInfo { get; set; }
        public string Ant115UserPosInfo { get; set; }
        public string Ant116UserPosInfo { get; set; }
        public string Ant117UserPosInfo { get; set; }
        public string Ant118UserPosInfo { get; set; }
        public string Ant119UserPosInfo { get; set; }
        public string Ant120UserPosInfo { get; set; }
        public string Ant121UserPosInfo { get; set; }
        public string Ant122UserPosInfo { get; set; }
        public string Ant123UserPosInfo { get; set; }
        public string Ant124UserPosInfo { get; set; }
        public string Ant125UserPosInfo { get; set; }
        public string Ant126UserPosInfo { get; set; }
        public string Ant127UserPosInfo { get; set; }
        public string Ant128UserPosInfo { get; set; }
    }

    public class AntRemarksInfo
    {
        public string Ant1RemarksInfo { get; set; }
        public string Ant2RemarksInfo { get; set; }
        public string Ant3RemarksInfo { get; set; }
        public string Ant4RemarksInfo { get; set; }
        public string Ant5RemarksInfo { get; set; }
        public string Ant6RemarksInfo { get; set; }
        public string Ant7RemarksInfo { get; set; }
        public string Ant8RemarksInfo { get; set; }
        public string Ant9RemarksInfo { get; set; }
        public string Ant10RemarksInfo { get; set; }
        public string Ant11RemarksInfo { get; set; }
        public string Ant12RemarksInfo { get; set; }
        public string Ant13RemarksInfo { get; set; }
        public string Ant14RemarksInfo { get; set; }
        public string Ant15RemarksInfo { get; set; }
        public string Ant16RemarksInfo { get; set; }
        public string Ant17RemarksInfo { get; set; }
        public string Ant18RemarksInfo { get; set; }
        public string Ant19RemarksInfo { get; set; }
        public string Ant20RemarksInfo { get; set; }
        public string Ant21RemarksInfo { get; set; }
        public string Ant22RemarksInfo { get; set; }
        public string Ant23RemarksInfo { get; set; }
        public string Ant24RemarksInfo { get; set; }
        public string Ant25RemarksInfo { get; set; }
        public string Ant26RemarksInfo { get; set; }
        public string Ant27RemarksInfo { get; set; }
        public string Ant28RemarksInfo { get; set; }
        public string Ant29RemarksInfo { get; set; }
        public string Ant30RemarksInfo { get; set; }
        public string Ant31RemarksInfo { get; set; }
        public string Ant32RemarksInfo { get; set; }
        public string Ant33RemarksInfo { get; set; }
        public string Ant34RemarksInfo { get; set; }
        public string Ant35RemarksInfo { get; set; }
        public string Ant36RemarksInfo { get; set; }
        public string Ant37RemarksInfo { get; set; }
        public string Ant38RemarksInfo { get; set; }
        public string Ant39RemarksInfo { get; set; }
        public string Ant40RemarksInfo { get; set; }
        public string Ant41RemarksInfo { get; set; }
        public string Ant42RemarksInfo { get; set; }
        public string Ant43RemarksInfo { get; set; }
        public string Ant44RemarksInfo { get; set; }
        public string Ant45RemarksInfo { get; set; }
        public string Ant46RemarksInfo { get; set; }
        public string Ant47RemarksInfo { get; set; }
        public string Ant48RemarksInfo { get; set; }
        public string Ant49RemarksInfo { get; set; }
        public string Ant50RemarksInfo { get; set; }
        public string Ant51RemarksInfo { get; set; }
        public string Ant52RemarksInfo { get; set; }
        public string Ant53RemarksInfo { get; set; }
        public string Ant54RemarksInfo { get; set; }
        public string Ant55RemarksInfo { get; set; }
        public string Ant56RemarksInfo { get; set; }
        public string Ant57RemarksInfo { get; set; }
        public string Ant58RemarksInfo { get; set; }
        public string Ant59RemarksInfo { get; set; }
        public string Ant60RemarksInfo { get; set; }
        public string Ant61RemarksInfo { get; set; }
        public string Ant62RemarksInfo { get; set; }
        public string Ant63RemarksInfo { get; set; }
        public string Ant64RemarksInfo { get; set; }
        public string Ant65RemarksInfo { get; set; }
        public string Ant66RemarksInfo { get; set; }
        public string Ant67RemarksInfo { get; set; }
        public string Ant68RemarksInfo { get; set; }
        public string Ant69RemarksInfo { get; set; }
        public string Ant70RemarksInfo { get; set; }
        public string Ant71RemarksInfo { get; set; }
        public string Ant72RemarksInfo { get; set; }
        public string Ant73RemarksInfo { get; set; }
        public string Ant74RemarksInfo { get; set; }
        public string Ant75RemarksInfo { get; set; }
        public string Ant76RemarksInfo { get; set; }
        public string Ant77RemarksInfo { get; set; }
        public string Ant78RemarksInfo { get; set; }
        public string Ant79RemarksInfo { get; set; }
        public string Ant80RemarksInfo { get; set; }
        public string Ant81RemarksInfo { get; set; }
        public string Ant82RemarksInfo { get; set; }
        public string Ant83RemarksInfo { get; set; }
        public string Ant84RemarksInfo { get; set; }
        public string Ant85RemarksInfo { get; set; }
        public string Ant86RemarksInfo { get; set; }
        public string Ant87RemarksInfo { get; set; }
        public string Ant88RemarksInfo { get; set; }
        public string Ant89RemarksInfo { get; set; }
        public string Ant90RemarksInfo { get; set; }
        public string Ant91RemarksInfo { get; set; }
        public string Ant92RemarksInfo { get; set; }
        public string Ant93RemarksInfo { get; set; }
        public string Ant94RemarksInfo { get; set; }
        public string Ant95RemarksInfo { get; set; }
        public string Ant96RemarksInfo { get; set; }
        public string Ant97RemarksInfo { get; set; }
        public string Ant98RemarksInfo { get; set; }
        public string Ant99RemarksInfo { get; set; }
        public string Ant100RemarksInfo { get; set; }
        public string Ant101RemarksInfo { get; set; }
        public string Ant102RemarksInfo { get; set; }
        public string Ant103RemarksInfo { get; set; }
        public string Ant104RemarksInfo { get; set; }
        public string Ant105RemarksInfo { get; set; }
        public string Ant106RemarksInfo { get; set; }
        public string Ant107RemarksInfo { get; set; }
        public string Ant108RemarksInfo { get; set; }
        public string Ant109RemarksInfo { get; set; }
        public string Ant110RemarksInfo { get; set; }
        public string Ant111RemarksInfo { get; set; }
        public string Ant112RemarksInfo { get; set; }
        public string Ant113RemarksInfo { get; set; }
        public string Ant114RemarksInfo { get; set; }
        public string Ant115RemarksInfo { get; set; }
        public string Ant116RemarksInfo { get; set; }
        public string Ant117RemarksInfo { get; set; }
        public string Ant118RemarksInfo { get; set; }
        public string Ant119RemarksInfo { get; set; }
        public string Ant120RemarksInfo { get; set; }
        public string Ant121RemarksInfo { get; set; }
        public string Ant122RemarksInfo { get; set; }
        public string Ant123RemarksInfo { get; set; }
        public string Ant124RemarksInfo { get; set; }
        public string Ant125RemarksInfo { get; set; }
        public string Ant126RemarksInfo { get; set; }
        public string Ant127RemarksInfo { get; set; }
        public string Ant128RemarksInfo { get; set; }
    }

    public class TableLayoutPanelTagDataView
    {
        public int MinColumn { get; set; }
        public int MinRow { get; set; }
        public int MaxColumn { get; set; }
        public int MaxRow { get; set; }
        public int PanelColumn { get; set; }
        public int PanelRow { get; set; }
        public string AntNumFont { get; set; }
        public int AntNumFontSize { get; set; }
        public string TagCountFont { get; set; }
        public int TagCountFontSize { get; set; }
    }
}
