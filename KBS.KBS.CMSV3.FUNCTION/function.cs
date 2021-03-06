﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using DevExpress.Data.Helpers;
using DevExpress.Internal;
using DevExpress.Xpo;
using KBS.KBS.CMSV3.DATAMODEL;
using NLog;
using Oracle.DataAccess.Client;
using Menu = KBS.KBS.CMSV3.DATAMODEL.Menu;
using System.Security.Cryptography;
using System.IO;

namespace KBS.KBS.CMSV3.FUNCTION
{
    public class function
    {
        private String ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private String ResultString;
        private OutputMessage outputMsg;

        OracleConnection con;

        /// <summary>
        /// Connects this instance.
        /// </summary>
        public void Connect()
        {
            try
            {
                logger.Trace("Start Starting Connection Server");
                con = new OracleConnection();
                con.ConnectionString = ConnectionString;
                logger.Debug("Connection String : " + con.ConnectionString.ToString());
                con.Open();
                logger.Debug("End Starting Connection Server");
            }
            catch (OracleException ex)
            {
                logger.Error("Connect Function");
                logger.Error(ex.Message);
                throw;
            }
        }


        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            try
            {
                logger.Debug("Closing Connection");
                con.Close();
                con.Dispose();
                logger.Debug("End Close Connection");
            }
            catch (Exception e)
            {
                logger.Error("Close Function");
                logger.Error(e.Message);
            }

        }


        /// <summary>
        /// Logins to SSC.
        /// </summary>
        /// <param name="UserID">The user id used to login.</param>
        /// <param name="Password">The password.</param>
        /// <returns></returns>
        public OutputMessage Login(String UserID, String Password)
        {
            OutputMessage message = new OutputMessage();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSUSER.LOGIN";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PUSERUSID", OracleDbType.Varchar2, 2000).Value = UserID;
                cmd.Parameters.Add("PUSERPASW", OracleDbType.Varchar2, 2000).Value = Password;
                //cmd.Parameters.Add("PUSERTYPE", OracleDbType.Varchar2).Value = "WEB";
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());


                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");

                message.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                message.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();
                //user.Status = (Member.UserStatus)((Int16)dr["STATUS"]);]

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return message;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }


        /// <summary>
        /// Logins to SSC.
        /// </summary> <param name="UserID">The user id used to login.</param>
        /// <param name="Password">The password.</param>
        /// <returns></returns>
        public User SelectUserFromUserID(String UserID)
        {
            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                    "select Cmsuser.Userusnm, Cmsuser.Userpasw, Cmsuser.Useracprof, Cmsuser.Usermeprof, Sitelink.Prststprof, Site.Sitesite,  Site.SITESITENAME, Site.Sitesclas from kdscmsuser cmsuser " +
                    "inner join kdscmsprofsitelink sitelink on Cmsuser.Userstprof = Sitelink.Prststprof " +
                    "inner join kdscmssite site on Sitelink.Prstsite = Site.Sitesite " +
                    "where cmsuser.userusid = :userid " +
                    "and rownum = 1 and site.SITESITEFLAG = 1" +
                    "order by Site.Sitesite, Site.Sitesite";


                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":userid", OracleDbType.Varchar2)).Value = UserID;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());



                OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                while (dr.Read())
                {
                    user.Username = (String)dr["Userusnm"];
                    user.Password = (String)dr["Userpasw"];
                    user.SiteProfile = (String)dr["Prststprof"];
                    user.AccessProfile = (String)dr["Useracprof"];
                    user.MenuProfile = (String)dr["Usermeprof"];
                    user.DefaultSite = (String)dr["Sitesite"];
                    user.DefaultSiteName = (String)dr["Sitesitename"];
                    user.SiteClass = dr["Sitesclas"].ToString();
                }

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return user;
            }
            catch (Exception e)
            {
                logger.Error("SelectUserDataFromUserID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public String ShowLicense()
        {
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                String Value = "Not Valid";
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select KDSCMSDLDESC as DATA from KDSCMSDL WHERE rownum = 1";

                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                OracleDataReader dr = cmd.ExecuteReader();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");

                while (dr.Read())
                {

                    Value = dr["DATA"].ToString();

                }
                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return Value;
            }
            catch (Exception e)
            {
                logger.Error("LicenseCheck");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public String LicenseCheck()
        {
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                String Value = "Not Valid";
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;                                
                cmd.CommandText = "select 'Valid' as Hasil from KDSCMSSITE WHERE SITESITESTATUS = 1 and rownum = 1";

                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                OracleDataReader dr = cmd.ExecuteReader();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");

                while (dr.Read())
                {

                    Value = dr["Hasil"].ToString();

                }
                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");                
                return Value;
            }
            catch (Exception e)
            {
                logger.Error("LicenseCheck");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "PT.KDSBS";
            string Hasil = "";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            try
            {
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                        Hasil = cipherText.ToString();
                    }
                }
                return Hasil;
            }
            catch
            {
                Hasil = "License Not Valid";
                return Hasil;
            }
        }

        public string GetLicense()
        {
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                String Value = "";
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select KDSCMSDLDESC from KDSCMSDL where KDSCMSDLID = 1";

                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                OracleDataReader dr = cmd.ExecuteReader();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");

                while (dr.Read())
                {
                    Value = dr["KDSCMSDLDESC"].ToString();
                }
                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return Value;
            }
            catch (Exception e)
            {
                logger.Error("GetLicense");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public License ParseLicenseText(string DecryptedText)
        {
            string[] values = DecryptedText.Split("|".ToCharArray());

            License license = new License();

            license.CompanyName = values[0];
            license.StoreTotal = values[1];
            license.Val1 = values[3];
            license.Val2 = values[4];
            license.Val3 = values[5];
            license.EndDate = DateTime.Parse(values[2]);

            return license;
        }

        public OutputMessage License(string LicenseData, string user)
        {

           
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSPARDTABLE.UPD_ODATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PPARDLIN", OracleDbType.Varchar2, 2000).Value = LicenseData;
                cmd.Parameters.Add("PKDSCMSDLCRBY", OracleDbType.Varchar2, 2000).Value = user;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("updateBrandHeader Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public OutputMessage DeleteStyleDetail(String IDGRP, String ID)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSTLTBL.DEL_DATA";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add("PSTYLSTGID", OracleDbType.Varchar2, 50).Value = IDGRP;
                cmd.Parameters.Add("PSTYLSTYLID", OracleDbType.Varchar2, 50).Value = ID;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("DeleteStyleDetail Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public OutputMessage DeleteStyleHeader(String IDGRP)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSTLGRP.DEL_DATABYSTGRSTGID";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add("PSTGRSTGID", OracleDbType.Varchar2, 50).Value = IDGRP;

                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage DeletePrice(String ID, String VAR, String SITE, DateTime DATE)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSPRICE.DEL_DATA";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add("PPSPRCITEMID", OracleDbType.Varchar2, 50).Value = ID;
                cmd.Parameters.Add("PSPRCVRNTID", OracleDbType.Varchar2, 50).Value = VAR;
                cmd.Parameters.Add("PSPRCSITE", OracleDbType.Varchar2, 50).Value = SITE;
                cmd.Parameters.Add("PSPRCSDAT", OracleDbType.Date).Value = DATE;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("DeletePrice Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage DeleteSizeDetail(String IDGRP, String ID)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSIZETBL.DEL_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PSIZESZGID", OracleDbType.Varchar2, 50).Value = IDGRP;
                cmd.Parameters.Add("PSIZESZID", OracleDbType.Varchar2, 50).Value = ID;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("DeleteSizeDetail Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public SKUGroup Cekdatasku(SKUGroup skugroup)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;


                cmd.CommandText =
                                  " select ROWNUM, 'NO' as Fail from KDSCMSSKULINK " +
                                  " where rownum = 1 and SKULINKSKUID = '" + skugroup.ID + "' ";

                logger.Debug(cmd.CommandText);
                OracleDataReader dr = cmd.ExecuteReader();



                while (dr.Read())
                {

                    skugroup.LDesc = dr["Fail"].ToString();

                }
                return skugroup;

            }
            catch (Exception e)
            {
                logger.Error("GetStyleDetailData Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        public OutputMessage DeleteSKUDetail(String IDGRP, String ID, String level)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSKUD.DEL_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PSKUDSKUID", OracleDbType.Int32).Value = IDGRP;
                cmd.Parameters.Add("PSKUDSKUIDD", OracleDbType.Int32).Value = ID;
                cmd.Parameters.Add("PSKUDLVL", OracleDbType.Int32).Value = level;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public OutputMessage DeleteSKUHeader(String ID)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSKUH.DEL_DATA";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add("PSKUHSKUID", OracleDbType.Int32).Value = ID;

                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }



        public string changePassword(String NewPassword, String UserID)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "UPDATE KDSCMSUSER SET USERPASW = '" + NewPassword + "', USERMDAT = SYSDATE, USERMOBY =  '" + UserID + "' WHERE USERUSID =  '" + UserID + "' ";
                cmd.CommandType = CommandType.Text;
                

                cmd.ExecuteNonQuery();

                ResultString = "Success";

                this.Close();
                return ResultString;
            }
            catch (Exception e)
            {
                logger.Error("changePassword Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }



        public List<Menu> SelectMenuByProfileIDandMenuGroup(String StoreProfile, String MenuGroupID)
        {
            Menu menu;
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");

            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                    "SELECT MENUMENUID AS MENUID, MENUMENUNM AS MENUNAME, MENUMENUID AS MENUNAMEID, MENUMEURL AS URLDESKTOP FROM KDSCMSMENU " +
                    "WHERE EXISTS(SELECT 1 FROM kdscmsmeprofd WHERE meprdmeprof = :ProfileId AND meprdmenuid = KDSCMSMENU.menumenuid) " +
                    "AND MENUMEGRPID = :MenuGroupID";


                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":ProfileId", OracleDbType.Varchar2)).Value = StoreProfile;
                cmd.Parameters.Add(new OracleParameter(":MenuGroupID", OracleDbType.Varchar2)).Value = MenuGroupID;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());



                OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                List<Menu> list = new List<Menu>();

                while (dr.Read())
                {
                    menu = new Menu();
                    menu.MenuID = dr["MENUID"].ToString();
                    menu.MenuName = dr["MENUNAME"].ToString();
                    menu.MenuURL = dr["URLDESKTOP"].ToString();
                    list.Add(menu);
                }



                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return list;
            }
            catch (Exception e)
            {
                logger.Error("SelectMenuByProfileIDandMenuGroup Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public String GetItemDescByItemID(String ItemID)
        {
            
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                    "select " +
                    "itemsdesc " +
                    "from kdscmsmstitem " +
                    "where itemitemid = :ItemID";


                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":ItemID", OracleDbType.Varchar2)).Value = ItemID;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());



                OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                String Item = "";

                while (dr.Read())
                {
                    Item = dr["itemsdesc"].ToString();
                }


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return Item;
            }
            catch (Exception e)
            {
                logger.Error("GetItemDescByItemID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public String GetVariantDescByVariantIDandItemID(String VariantID, String ItemID)
        {
            
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                    "select " +
                    "vrntldesc " +
                    "from Kdscmsmstvrnt " +
                    "where Vrntvrntid = :VariantID " +
                    "and vrntitemid = :ItemID";


                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":VariantID", OracleDbType.Int32)).Value = VariantID;
                cmd.Parameters.Add(new OracleParameter(":ItemID", OracleDbType.Int32)).Value = ItemID;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());



                OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                String Item = "";

                while (dr.Read())
                {
                    Item = dr["vrntldesc"].ToString();
                }


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return Item;
            }
            catch (Exception e)
            {
                logger.Error("GetVariantDescByVariantIDandItemID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public String ItemTypeVariant(String ItemIDExternal)
        {
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                    "select " +
                    "itemtype " +
                    "from kdscmsmstitem " +
                    "where itemitemidx = :ItemIDExternal";


                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":ItemIDExternal", OracleDbType.Varchar2)).Value = ItemIDExternal;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());



                OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                String Type = "";

                while (dr.Read())
                {
                    Type = dr["itemtype"].ToString();
                }


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return Type;
            }
            catch (Exception e)
            {
                logger.Error("GetItemIDByItemIDEx Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public String GetItemIDByItemIDEx(String ItemIDExternal)
        {
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                    "select " +
                    "itemitemid " +
                    "from kdscmsmstitem " +
                    "where itemitemidx = :ItemIDExternal";


                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":ItemIDExternal", OracleDbType.Varchar2)).Value = ItemIDExternal;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());



                OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                String Item = "";

                while (dr.Read())
                {
                    Item = dr["itemitemid"].ToString();
                }


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return Item;
            }
            catch (Exception e)
            {
                logger.Error("GetItemIDByItemIDEx Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public String GetVarIDByVarIDExandItemID(String VariantIDExternal, String ItemID)
        {
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                    "select Vrntvrntid " +
                    "from Kdscmsmstvrnt " +
                    "where Vrntvrntidx = :VariantIDExternal " +
                    "and VRNTITEMID = :ItemID";


                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":VariantIDExternal", OracleDbType.Varchar2)).Value = VariantIDExternal;
                cmd.Parameters.Add(new OracleParameter(":ItemID", OracleDbType.Varchar2)).Value = ItemID;

                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());



                OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                String VariantID = "";

                while (dr.Read())
                {
                    VariantID = dr["Vrntvrntid"].ToString();
                }


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return VariantID;
            }
            catch (Exception e)
            {
                logger.Error("GetVarIDByVarIDExandItemID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public String ClearSeason()
        {           
            return "Sukses";
        }
        public String GetVariantIDByVariantIDEx(String VariantIDExternal)
        {
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSMSTVRNT.GETVARIANTIDBYVARIANTIDEX";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.BindByName = true;

                cmd.Parameters.Add("PVRNTVRNTIDX", OracleDbType.Int32).Value = VariantIDExternal;
                cmd.Parameters.Add("Return_Value", OracleDbType.Int32).Direction = ParameterDirection.ReturnValue;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                String VariantID = "";
                cmd.ExecuteNonQuery();

                VariantID = cmd.Parameters["Return_Value"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return VariantID;
            }
            catch (Exception e)
            {
                logger.Error("GetVariantIDByVariantIDEx Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public List<Menu> SelectMenuGroupByProfileID(String StoreProfile)
        {
            Menu menu;
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");

            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT MENUMEGRPID as MENUGROUPID, MENUMEGRPNM AS MENUGROUPNAME FROM KDSCMSMENU " +
                                  "WHERE EXISTS(SELECT 1 FROM kdscmsmeprofd WHERE meprdmeprof = :ProfileId AND meprdmenuid = KDSCMSMENU.menumenuid) " +
                                  "GROUP BY MENUMEGRPID, MENUMEGRPNM ";


                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":ProfileId", OracleDbType.Varchar2)).Value = StoreProfile;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());



                OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                List<Menu> list = new List<Menu>();

                while (dr.Read())
                {
                    menu = new Menu();
                    menu.MenuGroupName = dr["MENUGROUPNAME"].ToString();
                    menu.MenuGroupID = dr["MENUGROUPID"].ToString();
                    list.Add(menu);
                }



                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return list;
            }
            catch (Exception e)
            {
                logger.Error("SelectMenuGroupByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public List<AccessContainer> SelectAccessByProfileAndMenuID(String AccessProfile, String MenuID)
        {
            AccessContainer accessCont;
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");

            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select Acprdfuncid, Acprdactype " +
                                  "from Kdscmsaccprofd " +
                                  "where Acprdacprof = :AccessProfile " +
                                  "and acprdmenuid = :MenuID";


                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":AccessProfile", OracleDbType.Varchar2)).Value = AccessProfile;
                cmd.Parameters.Add(new OracleParameter(":MenuID", OracleDbType.Varchar2)).Value = MenuID;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());



                OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                List<AccessContainer> list = new List<AccessContainer>();

                while (dr.Read())
                {
                    accessCont = new AccessContainer();
                    accessCont.FunctionId = dr["Acprdfuncid"].ToString();
                    accessCont.Type = dr["Acprdactype"].ToString();
                    list.Add(accessCont);
                }



                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return list;
            }
            catch (Exception e)
            {
                logger.Error("SelectMenuGroupByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }




        public DataTable GetAccessProfile()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select acprhacprof as ACCESSPROFILE, acprhacdesc as ACCESSPROFILEDESC from kdscmsaccprofh";
                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();
               

                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAccessProfile Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetSiteProfile()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select Stprstprof as SITEPROFILE, Stprstdesc as SITEPROFILEDESC from kdscmssiteprof";
                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteProfile Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetSite()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select Stprstprof as SITEPROFILE, Stprstdesc as SITEPROFILEDESC from kdscmssiteprof";
                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSite Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetSiteBySiteProfile(String Siteprofile)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select SITESITE, SITESITE || ' ' || SITESITENAME as SITESITENAME from KDSCMSSITE where exists " +
                                  "(select 1 from KDSCMSPROFSITELINK where PRSTSTPROF = :Siteprofile and PRSTSITE = KDSCMSSITE.SITESITE   ) and SITESITEFLAG = 1 and SITESITESTATUS = 1";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":Siteprofile", OracleDbType.Varchar2)).Value = Siteprofile;
                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteBySiteProfile Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllStoreAssortment(String SiteProfile, SiteMaster siteMaster)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                    "select KDSCMSSASS.SASSSITEID as SITE, KDSCMSSITE.SITESITENAME \"SITE NAME\", COUNT(KDSCMSSASS.SASSITEMID) as \"TOTAL STORE\" " +
                    "from KDSCMSSASS " +
                    "inner join KDSCMSSITE on KDSCMSSASS.SASSSITEID = KDSCMSSITE.SITESITE " +
                    "WHERE exists(select 1 from Kdscmsprofsitelink where prstsite = KDSCMSSASS.SASSSITEID and Kdscmsprofsitelink.Prststprof = :SiteProfile) ";
                    
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new OracleParameter(":SiteProfile", OracleDbType.Varchar2)).Value = SiteProfile;

                if (!string.IsNullOrWhiteSpace(siteMaster.Site))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and KDSCMSSASS.SASSSITEID like '%' || :Site || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":Site", OracleDbType.Varchar2)).Value = siteMaster.Site;
                }

                if (!string.IsNullOrWhiteSpace(siteMaster.SiteName))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and KDSCMSSITE.SITESITENAME like '%' || :SiteName || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":SiteName", OracleDbType.Varchar2)).Value = siteMaster.SiteName;
                }

                cmd.CommandText = cmd.CommandText + "GROUP BY KDSCMSSASS.SASSSITEID, KDSCMSSITE.SITESITENAME";


                

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAllStoreAssortment Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetUserStatusFromParameter()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select Meprhmeprof as \"MENU PROFILE\", Meprhmedesc as \"MENU PROFILE DESCRIPTION\"  from kdscmsmeprofh";
                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetUserStatusFromParameter Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetMenuProfile()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select Meprhmeprof as MENUPROFILE, Meprhmedesc as MENUPROFILEDESC  from kdscmsmeprofh";
                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetMenuProfile Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetSiteExcludeSiteProfile(String SiteProfile)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                    "select site.SITESITE, " +
                    "site.SITESITE || ' ' || site.SITESITENAME as SITESITENAME " +
                    "from kdscmssite site " +
                    "where not exists(select 1 from Kdscmsprofsitelink where prstsite = site.sitesite and Kdscmsprofsitelink.Prststprof = :SiteProfile)";

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":SiteProfile", OracleDbType.Varchar2)).Value = SiteProfile;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteExcludeSiteProfile Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetMenuExcludeMenuProfile(String MenuProfile)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                    "select Menu.Menumenuid, menu.menumenunm from kdscmsmenu menu " +
                    "where " +
                    "not exists(select 1 from Kdscmsmeprofd where  MEPRDMENUID = Menu.Menumenuid and Kdscmsmeprofd.Meprdmeprof = :MenuProfile)";

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":MenuProfile", OracleDbType.Varchar2)).Value = MenuProfile;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetMenuExcludeMenuProfile Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetParameterValueAndDescbyClassAndTabID(String SiteClass, String ParameterID)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select pardetail.pardtabent as PARVALUE, pardetail.pardtabent || '  ' || Pardetail.Pardldesc as PARDESCRIPTION " +
                                  "from kdscmsparhtable parHeader inner join kdscmspardtable parDetail on Parheader.Parhtabid = Pardetail.Pardtabid " +
                                  "where parHeader.parhtabid = " + ParameterID + " " +
                                  "and parHeader.parhsclas = " + SiteClass + " " +
                                  "and  Parheader.Parhsclas = Pardetail.Pardsclas";

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":Class", OracleDbType.Varchar2)).Value = SiteClass;
                cmd.Parameters.Add(new OracleParameter(":ParameterID", OracleDbType.Varchar2)).Value = ParameterID;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetParameterValueAndDescbyClassAndTabID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllBrand()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "Brndbrndid AS BRAND, " +
                                  "Brndbrndid || ' - ' || Brnddesc AS DESCRIPTION " +
                                  "from Kdscmsmstbrnd";

                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAllBrand Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllAssortment(string SiteClass, AssortmentMaster assortment)
        {
            try
            {
                
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select KDSCMSMSTITEM.ITEMITEMIDX as \"ITEM ID\", " +
                                  "KDSCMSMSTVRNT.VRNTVRNTIDX as VARIANT, " +
                                  "(SELECT Par.Pardldesc from Kdscmspardtable par where Par.Pardsclas = :SiteClas and Par.Pardtabid = 20 and Par.Pardtabent = KDSCMSSASS.Sassstat) as STATUS " +
                                  "from KDSCMSSASS inner join KDSCMSMSTVRNT on KDSCMSMSTVRNT.VRNTVRNTID = KDSCMSSASS.SASSVRNT " +
                                  "inner join KDSCMSMSTITEM on KDSCMSMSTVRNT.VRNTITEMID =  KDSCMSMSTITEM.ITEMITEMID " +
                                  "where KDSCMSMSTVRNT.VRNTITEMID = KDSCMSSASS.SASSITEMID " +
                                  "and KDSCMSSASS.SASSSITEID = :Site ";

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new OracleParameter(":SiteClas", OracleDbType.Varchar2)).Value = SiteClass;
                cmd.Parameters.Add(new OracleParameter(":Site", OracleDbType.Varchar2)).Value = assortment.Site;


                if (!string.IsNullOrWhiteSpace(assortment.ItemID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and KDSCMSMSTITEM.ITEMITEMIDX like '%' || :ItemID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":ItemID", OracleDbType.Varchar2)).Value = assortment.ItemID;
                }

                if (!string.IsNullOrWhiteSpace(assortment.VariantID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and KDSCMSMSTVRNT.VRNTVRNTIDX like '%' || :Variant || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":Variant", OracleDbType.Varchar2)).Value = assortment.VariantID;
                }

                if (!string.IsNullOrWhiteSpace(assortment.Status))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and KDSCMSSASS.SASSSTAT = :Status ";
                    cmd.Parameters.Add(new OracleParameter(":Status", OracleDbType.Varchar2)).Value = assortment.Status;
                }


                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAllAssortmentByVarIDandItemID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetItemNotExistInAssortment(string SiteClass, AssortmentMaster assortment)
        {
            try
            {

                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select KDSCMSMSTITEM.ITEMITEMIDX AS \"ITEM ID\", KDSCMSMSTVRNT.VRNTVRNTIDX AS VARIANT " +
                                  "from KDSCMSMSTITEM inner join KDSCMSMSTVRNT on KDSCMSMSTITEM.ITEMITEMID = KDSCMSMSTVRNT.VRNTITEMID " +
                                  "where " +
                                  "not exists" +
                                  "(select 1 from KDSCMSSASS where " +
                                  " KDSCMSSASS.SASSITEMID = KDSCMSMSTITEM.ITEMITEMID and " +
                                  " KDSCMSSASS.SASSSITEID = :Site AND " +
                                  " KDSCMSSASS.SASSVRNT = KDSCMSMSTVRNT.VRNTVRNTID) ";

                cmd.CommandType = CommandType.Text;

                
                cmd.Parameters.Add(new OracleParameter(":Site", OracleDbType.Varchar2)).Value = assortment.Site;


                if (!string.IsNullOrWhiteSpace(assortment.ItemID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and KDSCMSMSTITEM.ITEMITEMIDX like '%' || :ItemID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":ItemID", OracleDbType.Varchar2)).Value = assortment.ItemID;
                }

                if (!string.IsNullOrWhiteSpace(assortment.VariantID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and KDSCMSMSTVRNT.VRNTVRNTIDX like '%' || :Variant || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":Variant", OracleDbType.Varchar2)).Value = assortment.VariantID;
                }


                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetItemNotExistInAssortment Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
         public DataTable GetItemVariant( AssortmentMaster assortment)
        {
            try
            {

                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select KDSCMSMSTITEM.ITEMITEMID AS ITEMID, KDSCMSMSTITEM.ITEMITEMIDX AS \"ITEM ID\",KDSCMSMSTITEM.ITEMSDESC AS \"ITEM DESC\",  " +
                                  " KDSCMSMSTVRNT.VRNTVRNTID AS VARIANTID, KDSCMSMSTVRNT.VRNTVRNTIDX AS VARIANT , KDSCMSMSTVRNT.VRNTSDESC AS \"VARIANT DESC\" " +
                                  " from KDSCMSMSTITEM inner join KDSCMSMSTVRNT on KDSCMSMSTITEM.ITEMITEMID = KDSCMSMSTVRNT.VRNTITEMID ";

                cmd.CommandType = CommandType.Text;

                if (!string.IsNullOrWhiteSpace(assortment.ItemID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and KDSCMSMSTITEM.ITEMITEMIDX like '%' || :ItemID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":ItemID", OracleDbType.Varchar2)).Value = assortment.ItemID;
                }

                if (!string.IsNullOrWhiteSpace(assortment.VariantID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and KDSCMSMSTVRNT.VRNTVRNTIDX like '%' || :Variant || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":Variant", OracleDbType.Varchar2)).Value = assortment.VariantID;
                }


                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetItemNotExistInAssortment Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        public DataTable GetItemVariantBarcode(AssortmentMaster assortment)
        {
            try
            {

                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select KDSCMSMSTITEM.ITEMITEMID AS ITEMID, KDSCMSMSTITEM.ITEMITEMIDX AS \"ITEM ID\",KDSCMSMSTITEM.ITEMSDESC AS \"ITEM DESC\",  " +
                                  " KDSCMSMSTVRNT.VRNTVRNTID AS VARIANTID, KDSCMSMSTVRNT.VRNTVRNTIDX AS VARIANT , KDSCMSMSTVRNT.VRNTSDESC AS \"VARIANT DESC\" " +
                                  " from KDSCMSMSTITEM inner join KDSCMSMSTVRNT on KDSCMSMSTITEM.ITEMITEMID = KDSCMSMSTVRNT.VRNTITEMID where  " +
                                  "exists(select 1 from KDSCMSSASS where KDSCMSSASS.SASSITEMID = KDSCMSMSTITEM.ITEMITEMID and KDSCMSSASS.SASSSITEID = '"+ assortment.Site + "') ";

                cmd.CommandType = CommandType.Text;

                if (!string.IsNullOrWhiteSpace(assortment.ItemID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and KDSCMSMSTITEM.ITEMITEMIDX like '%' || :ItemID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":ItemID", OracleDbType.Varchar2)).Value = assortment.ItemID;
                }

                if (!string.IsNullOrWhiteSpace(assortment.VariantID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and KDSCMSMSTVRNT.VRNTVRNTIDX like '%' || :Variant || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":Variant", OracleDbType.Varchar2)).Value = assortment.VariantID;
                }


                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetItemNotExistInAssortment Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        public DataTable GetAllSizeGroup()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select Szgrszgid, " +
                                  "Szgrszgid||' - '|| Szgrdesc as Szgrdesc " +
                                  "from KDSCMSSIZEGRP";

                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAllSizeGroup Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllSizeDetailbySizeGroup(String SizeGroupID)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "sizeszid, " +
                                  "sizeszid ||' - '|| sizeldes as sizedesc " +
                                  "from Kdscmssizetbl " +
                                  "where Sizeszgid = :SizeGroupID";
                cmd.Parameters.Add(new OracleParameter(":SizeGroupID", OracleDbType.Varchar2)).Value = SizeGroupID;


                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAllSizeDetailbySizeGroup Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllStyleGroup()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select Stgrstgid, " +
                                  "Stgrstgid ||' - '|| Stgrdesc as stgrdesc " +
                                  "from Kdscmsstlgrp";

                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAllStyleGroup Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllStyleDetailbyStyleGroup(String StyleGroupID)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "Stylstylid, " +
                                  "Stylstylid ||' - '|| Stylldes as styldesc " +
                                  "from Kdscmsstltbl " +
                                  "where Stylstgid = :StyleGroupID";

                cmd.Parameters.Add(new OracleParameter(":StyleGroupID", OracleDbType.Varchar2)).Value = StyleGroupID;


                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAllStyleDetailbyStyleGroup Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllColorGroup()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "cogrcogid, " +
                                  "cogrcogid ||' - '|| cogrdesc as cogrdesc " +
                                  "from Kdscmscolgrp";

                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAllColorGroup Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllColorDetailbyColorGroup(String ColorGroupID)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "Colrcolid, " +
                                  "Colrcolid ||' - '|| Colrldes as colrdesc " +
                                  "from Kdscmscoltbl " +
                                  "where Colrcogid = :ColorGroupID";
                cmd.Parameters.Add(new OracleParameter(":ColorGroupID", OracleDbType.Varchar2)).Value = ColorGroupID;


                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAllColorDetailbyColorGroup Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }


        public DataTable GetParameterbyClassAndTabID(String SiteClass, String ParameterID)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select pardetail.pardtabent as PARVALUE, Pardetail.Pardldesc as PARDESCRIPTION " +
                                  "from kdscmsparhtable parHeader inner join kdscmspardtable parDetail on Parheader.Parhtabid = Pardetail.Pardtabid " +
                                  "where parHeader.parhtabid = " + ParameterID + " " +
                                  "and parHeader.parhsclas = " + SiteClass + " " +
                                  "and  Parheader.Parhsclas = Pardetail.Pardsclas";

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":Class", OracleDbType.Varchar2)).Value = SiteClass;
                cmd.Parameters.Add(new OracleParameter(":ParameterID", OracleDbType.Varchar2)).Value = ParameterID;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetParameterbyClassAndTabID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetListBoxAccessProfileDetail(String SiteClass, String MenuID, String AccessProfileID)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText =
                    "select " +
                    "nvl((" +
                        "select accProfD.Acprdactype " +
                        "from kdscmsaccprofd accProfD " +
                        "where param.pardtabent = AccprofD.Acprdfuncid " +
                        "and NVL(accprofd.Acprdmenuid, '" + MenuID + "') = '" + MenuID + "' " +
                        "and Accprofd.Acprdacprof = '" + AccessProfileID + "' " +
                    "),0) AccPermission , " +
                        "param.PARDLDESC as AccDesc " +
                    "from kdscmspardtable param " +
                    "where pardtabid = 4 " +
                    "and pardsclas = '" + SiteClass + "'";

                cmd.CommandType = CommandType.Text;             

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetListBoxAccessProfileDetail Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public ParameterHeader GetParameterHeader(ParameterHeader paramHeader)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT " +
                                  "PARHTABNM, " +
                                  "PARHSCLAS, " +
                                  "PARHCOPY, " +
                                  "PARHTABCOM, " +
                                  "PARHTABLOCK, " +
                                  "PARHCDAT, " +
                                  "PARHMDAT, " +
                                  "PARHCRBY, " +
                                  "PARHMOBY, " +
                                  "PARHNMOD " +
                                  "FROM KDSCMSPARHTABLE " +
                                  "where PARHTABID = :Id " +
                                  "and parhsclas = :SClass";


                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":Id", OracleDbType.Int32)).Value = paramHeader.ID;
                cmd.Parameters.Add(new OracleParameter(":SClass", OracleDbType.Int32)).Value = paramHeader.SClass;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    paramHeader.Name = dr["PARHTABNM"].ToString();
                    paramHeader.SClass = dr["PARHSCLAS"].ToString();
                    paramHeader.Copy = dr["PARHCOPY"].ToString();
                    paramHeader.Comment = dr["PARHTABCOM"].ToString();
                    paramHeader.Lock = dr["PARHTABLOCK"].ToString();
                }

                this.Close();
                return paramHeader;
            }
            catch (Exception e)
            {
                logger.Error("GetParameterHeader Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public ItemMaster GetItemMaster(ItemMaster item)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "ITEMITEMIDX, " +
                                  "ITEMTYPE, " +
                                  "ITEMSDESC, " +
                                  "ITEMLDESC, " +
                                  "ITEMBRNDID " +
                                  "from Kdscmsmstitem " +
                                  "where ITEMITEMID = :ItemID";


                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":ItemID", OracleDbType.Int32)).Value = item.ItemID;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    item.ItemIDExternal = dr["ITEMITEMIDX"].ToString();
                    item.Type = dr["ITEMTYPE"].ToString();
                    item.ShortDesc = dr["ITEMSDESC"].ToString();
                    item.LongDesc = dr["ITEMLDESC"].ToString();
                    item.Brand = dr["ITEMBRNDID"].ToString();
                }

                this.Close();
                return item;
            }
            catch (Exception e)
            {
                logger.Error("GetItemMaster Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public VariantMaster GetVariantMaster(VariantMaster variant)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "Vrntsdesc, " +
                                  "Vrntldesc, " +
                                  "Vrntstat " +
                                  "from Kdscmsmstvrnt " +
                                  "where VrntvrntidX = :VariantIDEx " +
                                  "and Vrntitemid = :ItemID";
                
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new OracleParameter(":VariantIDEx", OracleDbType.Int32)).Value = variant.VariantIDExternal;
                cmd.Parameters.Add(new OracleParameter(":ItemID", OracleDbType.Int32)).Value = variant.ItemID;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    variant.LongDesc = dr["Vrntldesc"].ToString();
                    variant.ShortDesc = dr["Vrntsdesc"].ToString();
                    variant.Status = dr["Vrntstat"].ToString();
                }

                this.Close();
                return variant;
            }
            catch (Exception e)
            {
                logger.Error("GetVariantMaster Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public BarcodeMaster GetBarcode(BarcodeMaster barcode)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "BRCDSDAT, " +
                                  "BRCDEDAT, " +
                                  "Brcdtype, " +
                                  "Brcdstat " +
                                  "from KDSCMSMSTBRCD " +
                                  "where Brcditemid = :ItemID " +
                                  "and Brcdvrntid = :VariantID " +
                                  "and Brcdbrcdid = :Barcode";


                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new OracleParameter(":ItemID", OracleDbType.Int32)).Value = barcode.ItemID;
                cmd.Parameters.Add(new OracleParameter(":VariantID", OracleDbType.Int32)).Value = barcode.VariantID;
                cmd.Parameters.Add(new OracleParameter(":Barcode", OracleDbType.Varchar2)).Value = barcode.Barcode;


                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    barcode.StartDate = DateTime.Parse(dr["BRCDSDAT"].ToString());
                    barcode.EndDate = DateTime.Parse(dr["BRCDEDAT"].ToString());
                    barcode.Status = dr["Brcdstat"].ToString();
                    barcode.Type = dr["Brcdtype"].ToString();

                }

                this.Close();
                return barcode;
            }
            catch (Exception e)
            {
                logger.Error("GetBarcode Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public String GetParameterEntryFromLongDescription(String LongDesc, String SiteClass, String ParameterDetailId)
        {
            try
            {

                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select pardtabent " +
                                  "from Kdscmspardtable " +
                                  "where pardldesc = :LDesc " +
                                  "and pardtabid = :ParDId " +
                                  "and pardsclas = :SClass";


                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":LDesc", OracleDbType.Varchar2)).Value = LongDesc;
                cmd.Parameters.Add(new OracleParameter(":ParDId", OracleDbType.Int32)).Value = ParameterDetailId;
                cmd.Parameters.Add(new OracleParameter(":SClass", OracleDbType.Int32)).Value = SiteClass;


                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                String Entry = null;

                while (dr.Read())
                {
                    Entry = dr["pardtabent"].ToString();
                }

                this.Close();
                return Entry;
            }
            catch (Exception e)
            {
                logger.Error("GetParameterEntryFromLongDescription Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        //Brand 

        public OutputMessage updateBrandHeader(BrandGroup brandgroup, String User)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSMSTBRND.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PBRNDBRNDID", OracleDbType.Varchar2, 3).Value = brandgroup.ID;
                cmd.Parameters.Add("PBRNDDESC", OracleDbType.Varchar2, 50).Value = brandgroup.Brand;
                cmd.Parameters.Add("PBRNDMOBY", OracleDbType.Varchar2, 20).Value = User;
                cmd.Parameters.Add("PBRNDINTF", OracleDbType.Int32, 1).Value = 1;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;
                
                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("updateBrandHeader Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage InsertBrandGroup(BrandGroup brandgroup, String User)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSMSTBRND.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PBRNDBRNDID", OracleDbType.Varchar2, 50).Value = brandgroup.ID;
                cmd.Parameters.Add("PBRNDDESC", OracleDbType.Varchar2, 50).Value = brandgroup.Brand;
                cmd.Parameters.Add("PBRNDCRBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("PBRNDINTF", OracleDbType.Int32, 50).Value = 1;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;

                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("InsertBrandGroup Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage InsertBarcode(BarcodeMaster barcode, String User)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSMSTBRCD.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add("PBRCDBRCDID", OracleDbType.Varchar2, 20).Value = barcode.Barcode;
                cmd.Parameters.Add("PBRCDITEMID", OracleDbType.Int32).Value = barcode.ItemID;
                cmd.Parameters.Add("PBRCDVRNTID", OracleDbType.Int32).Value = barcode.VariantID;
                cmd.Parameters.Add("PBRCDTYPE", OracleDbType.Int32).Value = barcode.Type;
                cmd.Parameters.Add("PBRCDSTAT", OracleDbType.Int32).Value = barcode.Status;
                cmd.Parameters.Add("PBRCDSDAT", OracleDbType.Date).Value = barcode.StartDate;
                cmd.Parameters.Add("PBRCDEDAT", OracleDbType.Date).Value = barcode.EndDate;
                cmd.Parameters.Add("PBRNDCRBY", OracleDbType.Varchar2, 20).Value = User;
                cmd.Parameters.Add("PBRNDINTF", OracleDbType.Varchar2, 1).Value = "1";
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;

                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();                
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("InsertBarcode Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage UpdateBarcode(BarcodeMaster barcode, String User)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSMSTBRCD.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add("PBRCDBRCDID", OracleDbType.Varchar2, 50).Value = barcode.Barcode;
                cmd.Parameters.Add("PBRCDITEMID", OracleDbType.Int32).Value = barcode.ItemID;
                cmd.Parameters.Add("PBRCDVRNTID", OracleDbType.Int32).Value = barcode.VariantID;
                cmd.Parameters.Add("PBRCDTYPE", OracleDbType.Int32).Value = barcode.Type;
                cmd.Parameters.Add("PBRCDSTAT", OracleDbType.Int32).Value = barcode.Status;
                cmd.Parameters.Add("PBRCDSDAT", OracleDbType.Date).Value = barcode.StartDate;
                cmd.Parameters.Add("PBRCDEDAT", OracleDbType.Date).Value = barcode.EndDate;
                cmd.Parameters.Add("PBRNDMOBY", OracleDbType.Varchar2, 20).Value = User;
                cmd.Parameters.Add("PBRNDINTF", OracleDbType.Varchar2, 1).Value = "1";
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;
                
                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("UpdateBarcode Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public DataTable GetInterfaceDataTable()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;

                cmd.CommandText = "select INTID as ID, INTDESC as Description, INTLSTRUN as \"Last Run\", INTLSTBY as \"Executed By\" from KDSCMSMSTINT ";

                
                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;}
            catch (Exception e)
            {
                logger.Error("GetInterfaceDataTable Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetInterfaceSite()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;

                cmd.CommandText = "SELECT " +
                                  "ROWID, " +
                                  "INTSITESITE as SITE," +
                                  "INTSITESCLAS as SITECLASS, " +
                                  "INTSITESITENAME as SITENAME, " +
                                  "INTSITESITECDAT as \"CREATED DATE\", " +
                                  "INTSITESITEMDAT as \"MODIFIED DATE\", " +
                                  "INTSITESITECRBY as \"CREATED BY\", " +
                                  "INTSITESITEMOBY as \"MODIFIED BY\", " +
                                  "INTSITESITEMSG as MESSAGE, " +
                                  "INTSITESITEFLAG as FLAG," +
                                  "INTSITESITEINTF as \"INTERFACE FLAG\" " +
                                  "FROM KDSCMSINTSITE " +
                                  "where INTSITESITEINTF < 1";


                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetInterfaceSite Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetInterfaceItem()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;

                cmd.CommandText = "SELECT ROWID,  " +
                                  "INTITEMITEMIDX AS \"ITEM ID EXTERNAL\", " +
                                  "INTITEMTYPE AS \"TYPE\", " +
                                  "INTITEMSDESC AS \"SHORT DESC\", " +
                                  "INTITEMLDESC AS \"LONG DESC\", " +
                                  "INTITEMBRNDID AS \"BRAND\", " +
                                  "INTITEMCDAT AS \"CREATED DATE\", " +
                                  "INTITEMMDAT AS \"MODIFIED DATE\", " +
                                  "INTITEMCRBY AS \"CREATED BY\", " +
                                  "INTITEMMOBY AS \"MODIFIED BY\", " +
                                  "INTITEMMSG AS \"MESSAGE\"," +
                                  "INTITEMINTF AS \"INTERFACE FLAG\" " +
                                  "FROM KDSCMSINTMSTITEM " +
                                  "WHERE INTITEMINTF < 1 ";


                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetInterfaceSite Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetInterfaceVariant()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;

                cmd.CommandText = "SELECT ROWID, " +
                                  "INTDTLVRNTVRNTID as \"VARIANT\", " +
                                  "INTDTLVRNTITMID as  \"ITEM ID\", " +
                                  "INTDTLVRNTSZGID as \"SIZE GROUP\", " +
                                  "INTDTLVRNTSZID as \"SIZE\", " +
                                  "INTDTLVRNTCOGID as \"COLOR GROUP\", " +
                                  "INTDTLVRNTCOLID as \"COLOR\", " +
                                  "INTDTLVRNTSTGID as \"STYLE GROUP\", " +
                                  "INTDTLVRNTSTYLID as \"STYLE\", " +
                                  "INTDTLVRNTCDAT as \"CREATED DATE\", " +
                                  "INTDTLVRNTMDAT as \"MODIFIED DATE\", " +
                                  "INTDTLVRNTCRBY as \"CREATED BY\", " +
                                  "INTDTLVRNTMOBY as \"MODIFIED BY\", " +
                                  "INTDTLVRNTMSG as \"MESSAGE\"," +
                                  "INTDTLVRNTSDESC as \"SHORT DESC\"," +
                                  "INTDTLVRNTLDESC as \"LONG DESC\"," +
                                  "INTDTLVRNTDINTF as  \"INTERFACE FLAG\" " +
                                  "FROM KDSCMSINTDTLVRNT " +
                                  "where INTDTLVRNTDINTF < 1 ";


                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetInterfaceVariant Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetInterfaceDN()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;

                cmd.CommandText = "SELECT ROWID, " +
                                  "CMSID AS \"CMS ID\", " +
                                  "CMSITEMID AS \"ITEM ID\", " +
                                  "CMSBARCODE AS \"BARCODE\", " +
                                  "CMSDESC AS \"DESCRIPTION\", " +
                                  "CMSQTY AS \"QTY\", " +
                                  "CMSPRICE AS \"PRICE\", " +
                                  "CMSSTORE AS \"STORE\", " +
                                  "CMSUSERID AS \"USER ID\", " +
                                  "CMSDATE AS \"CREATED DATE\", " +
                                  "CMSMSG AS MESSAGE, " +
                                  "CMSINTF AS \"INTERFACE FLAG\" " +
                                  "FROM KDSCMSDN_INT " +
                                  "WHERE CMSFLAG < 1 ";


                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetInterfaceDN Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetInterfacePriceAssortment()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;

                cmd.CommandText = "SELECT ROWID, " +
                                  "INTSPRCITEMIDX AS \"ITEM ID\", " +
                                  "INTSPRCDVRNTIDX AS \"VARIANT ID\", " +
                                  "INTSPRCSITE AS \"SITE\", " +
                                  "INTSPRCSPRICE AS \"SALES PRICE\", " +
                                  "INTSPRCVAT AS \"VAT\", " +
                                  "INTSPRCSDAT AS \"START DATE\", " +
                                  "INTSPRCEDAT AS \"END DATE\", " +
                                  "INTFILENAME AS \"FILENAME\", " +
                                  "INTERRMESS AS \"MESSAGE\", " +
                                  "INTDCRE AS \"CREATED DATE\", " +
                                  "INTDMAJ AS \"MODIFIED DATE\", " +
                                  "INTUTIL AS \"MODIFIED BY\", " +
                                  "INTTRT AS  \"INTERFACE FLAG\"," +
                                  "INTASSSTAT AS \"ASSORTMENT STATUS\"" +
                                  "FROM KDSCMSINTSPRICEASS " +
                                  "WHERE INTTRT < 1 ";


                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetInterfacePriceAssortment Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetInterfaceBarcode()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;

                cmd.CommandText = "SELECT ROWID, " +
                                  "INTBRCDBRCDID AS BARCODE, " +
                                  "INTBRCDITEMID AS \"ITEM ID\", " +
                                  "INTBRCDVRNTID AS \"VARIANT ID\", " +
                                  "INTBRCDTYPE AS \"TYPE\", " +
                                  "INTBRCDSTAT AS \"STATUS\", " +
                                  "INTBRCDSDAT AS \"START DATE\", " +
                                  "INTBRCDEDAT AS \"END DATE\", " +
                                  "INTBRCDCDAT AS \"CREATED DATE\", " +
                                  "INTBRCDMDAT AS \"MODIFIED DATE\", " +
                                  "INTBRCDCRBY AS \"CREATED BY\", " +
                                  "INTBRCDMOBY AS \"MODIFIED BY\", "+
                                  "INTBRCDMSG AS MESSAGE," +
                                  "INTBRCDINTF AS  \"INTERFACE FLAG\" " +
                                  "FROM KDSCMSINTMSTBRCD " +
                                  "WHERE INTBRCDINTF < 1 ";


                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetInterfaceBarcode Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetInterfaceAssortment()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;

                cmd.CommandText = "SELECT ROWID, " +
                                  "INTSASSITEMID AS \"ITEM ID\", " +
                                  "INTSASSSITEID AS \"SITE\"," +
                                  "INTSASSVRNT AS  \"VARIANT\"," +
                                  "INTSASSSTAT AS  \"STATUS\"," +
                                  "INTSASSCDAT AS \"CREATED DATE\", " +
                                  "INTSASSMDAT AS \"MODIFIED DATE\", " +
                                  "INTSASSCRBY AS \"CREATED BY\", " +
                                  "INTSASSMOBY AS \"MODIFIED BY\", " +
                                  "INTSASSMSG AS MESSAGE," +
                                  "INTSASSINTF AS  \"INTERFACE FLAG\" " +
                                  "FROM KDSCMSINTSASS " +
                                  "WHERE INTSASSINTF < 1 ";


                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetInterfaceAssortment Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public string getIncomingDirectory()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;

                    cmd.CommandText =
                                     "select DIRECTORY_PATH from ALL_DIRECTORIES where DIRECTORY_NAME = 'SOURCE_DIR'";
                    
                logger.Debug(cmd.CommandText);
                OracleDataReader dr = cmd.ExecuteReader();

                string Result = "";

                while (dr.Read())
                {

                    Result = dr["DIRECTORY_PATH"].ToString();

                }
                return Result;

            }
            catch (Exception e)
            {
                logger.Error("getIncomingDirectory Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public string getSiteMasterDirectory()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;

                cmd.CommandText =
                                 "select DIRECTORY_PATH from ALL_DIRECTORIES where DIRECTORY_NAME = 'SITE_MASTER_DIR'";

                logger.Debug(cmd.CommandText);
                OracleDataReader dr = cmd.ExecuteReader();

                string Result = "";

                while (dr.Read())
                {

                    Result = dr["DIRECTORY_PATH"].ToString();

                }
                return Result;

            }
            catch (Exception e)
            {
                logger.Error("getIncomingDirectory Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }


        public OutputMessage ExecuteSPProcessBarcode()
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "KDS_PROCESS_INTERFACE_BARCODE";
                cmd.CommandType = CommandType.StoredProcedure;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("ExecuteSPProcessBarcode Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage ExecuteSPProcessSite()
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "KDS_PROCESS_INTERFACE_STORE";
                cmd.CommandType = CommandType.StoredProcedure;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                
                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("ExecuteSPProcessSite Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage ExecuteSPProcessVariant()
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "KDS_PROCESS_INTERFACE_VARIANT";
                cmd.CommandType = CommandType.StoredProcedure;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("ExecuteSPProcessVariant Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage ExecuteSPProcessItem()
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "KDS_PROCESS_INTERFACE_ITEM";
                cmd.CommandType = CommandType.StoredProcedure;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("ExecuteSPProcessItem Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage ExecuteSPProcessDN()
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "KDS_PROCESS_INTERFACE_DN";
                cmd.CommandType = CommandType.StoredProcedure;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("ExecuteSPProcessDN Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage ExecuteSPProcessPrice()
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "KDS_PROCESS_INTERFACE_PRICE";
                cmd.CommandType = CommandType.StoredProcedure;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("ExecuteSPProcessPrice Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage ExecuteSPImportStoreFile(String FileName)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "Import_STORE_File";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("P_FILENAME", OracleDbType.Varchar2).Value = FileName;
                cmd.Parameters.Add("o_msg", OracleDbType.Varchar2).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                //outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["o_msg"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("ExecuteSPImportStoreFile Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage ExecuteSPImportItemFile(String FileName)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "Import_ITEM_File";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("P_FILENAME", OracleDbType.Varchar2).Value = FileName;
                cmd.Parameters.Add("o_msg", OracleDbType.Varchar2).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                //outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["o_msg"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("ExecuteSPImportItemFile Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage ExecuteSPImportVariantFile(String FileName)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "Import_VARIANT_File";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("P_FILENAME", OracleDbType.Varchar2).Value = FileName;
                cmd.Parameters.Add("o_msg", OracleDbType.Varchar2).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                //outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["o_msg"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("ExecuteSPImportVariantFile Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage ExecuteSPImportBarcodeFile(String FileName)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "Import_BARCODE_File";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("P_FILENAME", OracleDbType.Varchar2).Value = FileName;
                cmd.Parameters.Add("o_msg", OracleDbType.Varchar2).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                //outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["o_msg"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("ExecuteSPImportBarcodeFile Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage ExecuteSPImportAssortmentFile(String FileName)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "Import_assortment_File";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("P_FILENAME", OracleDbType.Varchar2).Value = FileName;
                cmd.Parameters.Add("o_msg", OracleDbType.Varchar2).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                //outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["o_msg"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("ExecuteSPImportAssortmentFile Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage ExecuteSPImportPriceAssortmentFile(String FileName)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "Import_PRICE_ASSORTMENT_File";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("P_FILENAME", OracleDbType.Varchar2).Value = FileName;
                cmd.Parameters.Add("o_msg", OracleDbType.Varchar2).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                //outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["o_msg"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("ExecuteSPImportPriceAssortmentFile Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage ExecuteSPImportDNFile(String FileName)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "IMPORT_DN_FILE";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("P_FILENAME", OracleDbType.Varchar2).Value = FileName;
                cmd.Parameters.Add("o_msg", OracleDbType.Varchar2).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                //outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["o_msg"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("ExecuteSPImportDNFile Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }



        public DataTable GetBrandHeaderDataTable(BrandGroup brandgroup)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;

                cmd.CommandText = "SELECT BRNDBRNDID AS \"ID\", " +
                                  "BRNDDESC AS \"BRAND DESC\" " +
                                  "FROM KDSCMSMSTBRND " +
                                  "where BRNDBRNDID is not null ";

                if (!string.IsNullOrWhiteSpace(brandgroup.ID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and BRNDBRNDID like '%' || :ID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":ID", OracleDbType.Varchar2)).Value = brandgroup.ID;
                }

                if (!string.IsNullOrWhiteSpace(brandgroup.Brand))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and BRNDDESC like '%' || :DESCRIPTION || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":DESCRIPTION", OracleDbType.Varchar2)).Value = brandgroup.Brand.ToString();
                }
                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetBrandHeaderDataTable Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        public string cekBrand(string brandid, string datacek)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;

                if (datacek == "1")
                {
                    cmd.CommandText =
                                     " select ROWNUM, 'NO' as Fail from KDSCMSMSTBRND " +
                                     " where rownum = 1 and BRNDBRNDID = '" + brandid + "' and BRNDBRNDID in (select distinct ITEMBRNDID from KDSCMSMSTITEM) ";

                }
                else
                {
                    cmd.CommandText =
                                      " select ROWNUM, 'NO' as Fail from KDSCMSMSTBRND " +
                                      " where rownum = 1 and BRNDBRNDID = '" + brandid + "' OR BRNDDESC = '" + datacek + "' ";
                }
                logger.Debug(cmd.CommandText);
                OracleDataReader dr = cmd.ExecuteReader();

                string Result = "";

                while (dr.Read())
                {

                    Result = dr["Fail"].ToString();

                }
                return Result;

            }
            catch (Exception e)
            {
                logger.Error("Get Brand ID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        //End Brand

        //SKULink

        public string cekColor(string ColorId, string datacek, string  status )
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;

                if (status == "Delete")
                {
                    cmd.CommandText =
                                     " select ROWNUM, 'NO' as Fail from KDSCMSCOLGRP " +
                                     " where rownum = 1 and COGRCOGID = '" + ColorId + "' and COGRCOGID in (select distinct COLRCOGID from KDSCMSCOLTBL) ";
                   
                }
                else if (status == "Insert")
                {
                    cmd.CommandText =
                                      " select ROWNUM, 'NO' as Fail from KDSCMSCOLGRP " +
                                      " where rownum = 1 and COGRCOGID = '" + ColorId + "' ";
                }
                else
                {
                    cmd.CommandText =
                                      " select ROWNUM, 'NO' as Fail from KDSCMSCOLTBL " +
                                      " where rownum = 1 and COLRCOGID = '" + ColorId + "' and (COLRORDR = '" + status + "' or COLRCOLID = '" + datacek + "' ) ";
                }
                logger.Debug(cmd.CommandText);
                OracleDataReader dr = cmd.ExecuteReader();

                string Result = "";

                while (dr.Read())
                {

                    Result = dr["Fail"].ToString();

                }
                return Result;

            }
            catch (Exception e)
            {
                logger.Error("Get Brand ID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        public string CekAccess(string Data)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;

               
                    cmd.CommandText =
                                      " select ROWNUM, 'NO' as Fail from KDSCMSACCPROFH " +
                                      " where rownum = 1 and ACPRHACPROF = '" + Data + "' and ACPRHACPROF in (select distinct USERACPROF from KDSCMSUSER )";
                
                logger.Debug(cmd.CommandText);
                OracleDataReader dr = cmd.ExecuteReader();

                string Result = "";

                while (dr.Read())
                {

                    Result = dr["Fail"].ToString();

                }
                return Result;

            }
            catch (Exception e)
            {
                logger.Error("Get Brand ID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        public string CekVATParam (string status)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;

                
                    cmd.CommandText =
                                      " select PARDVAN2 from KDSCMSPARDTABLE WHERE PARDLDESC = 'VAT' and PARDTABID = '17' ";
                
                logger.Debug(cmd.CommandText);
                OracleDataReader dr = cmd.ExecuteReader();

                string Result = "0";

                while (dr.Read())
                {

                    Result = dr["PARDVAN2"].ToString();

                }
                return Result;

            }
            catch (Exception e)
            {
                logger.Error("Get VAT Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        public string cekStyle(string GroupId, string datacek, string status)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;

                if (status == "Delete")
                {
                    cmd.CommandText =
                                     " select ROWNUM, 'NO' as Fail from KDSCMSSTLGRP " +
                                     " where rownum = 1 and STGRSTGID = '" + GroupId + "' and STGRSTGID in (select distinct STYLSTGID from KDSCMSSTLTBL) ";

                }
                else if (status == "Insert")
                {
                    cmd.CommandText =
                                      " select ROWNUM, 'NO' as Fail from KDSCMSSTLGRP " +
                                      " where rownum = 1 and STGRSTGID = '" + GroupId + "' ";
                }
                else
                {
                    cmd.CommandText =
                                      " select ROWNUM, 'NO' as Fail from KDSCMSSTLTBL " +
                                      " where rownum = 1 and STYLSTGID = '" + GroupId + "' and (STYLORDR = '" + status + "' or STYLSTYLID = '" + datacek + "' ) ";
                }
                logger.Debug(cmd.CommandText);
                OracleDataReader dr = cmd.ExecuteReader();

                string Result = "";

                while (dr.Read())
                {

                    Result = dr["Fail"].ToString();

                }
                return Result;

            }
            catch (Exception e)
            {
                logger.Error("Get Brand ID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        public string cekSize(string GroupId, string datacek, string status)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;

                if (status == "Delete")
                {
                    cmd.CommandText =
                                     " select ROWNUM, 'NO' as Fail from KDSCMSSIZEGRP " +
                                     " where rownum = 1 and SZGRSZGID = '" + GroupId + "' and SZGRSZGID in (select distinct SIZESZGID from KDSCMSSIZETBL) ";

                }
                else if (status == "Insert")
                {
                    cmd.CommandText =
                                      " select ROWNUM, 'NO' as Fail from KDSCMSSIZEGRP " +
                                      " where rownum = 1 and SZGRSZGID = '" + GroupId + "' ";
                }
                else
                {
                    cmd.CommandText =
                                      " select ROWNUM, 'NO' as Fail from KDSCMSSIZETBL " +
                                      " where rownum = 1 and SIZESZGID = '" + GroupId + "' and (SIZEORDR = '" + status + "' or SIZESZID = '" + datacek + "' ) ";
                }
                logger.Debug(cmd.CommandText);
                OracleDataReader dr = cmd.ExecuteReader();

                string Result = "";

                while (dr.Read())
                {

                    Result = dr["Fail"].ToString();

                }
                return Result;

            }
            catch (Exception e)
            {
                logger.Error("Get Brand ID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        public SKULink Cekdataskulink(SKULink skulink)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;


                cmd.CommandText =
                                  " select ROWNUM, 'NO' as Fail from KDSCMSSLSD " +
                                  " where rownum = 1 and SLSDSITE = '" + skulink.SITE + "' and SLSDSKUID = '" + skulink.SKU + "' ";

                logger.Debug(cmd.CommandText);
                OracleDataReader dr = cmd.ExecuteReader();



                while (dr.Read())
                {

                    skulink.BRAND = dr["Fail"].ToString();

                }
                return skulink;

            }
            catch (Exception e)
            {
                logger.Error("GetStyleDetailData Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetVariantByAssortment(String Itemid, String SITE)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = " select VRNTVRNTID as VALUE, VRNTVRNTIDX as DESCRIPTION from KDSCMSSASS, KDSCMSMSTVRNT " +
                                  " where SASSITEMID = VRNTITEMID AND SASSVRNT = VRNTVRNTID " +
                                  " AND SASSCDAT <= SYSDATE  AND VRNTITEMID = '" + Itemid + "' " +
                                  " AND SASSSITEID = '" + SITE + "' GROUP BY VRNTVRNTID, VRNTVRNTIDX ";

                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAccessProfile Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        public DataTable GetItemByAssortment(String SITE)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select ITEMITEMID as VALUE, ITEMITEMIDX as DESCRIPTION from KDSCMSSASS, KDSCMSMSTITEM  " +
                                  " where SASSITEMID = ITEMITEMID AND SASSCDAT <= SYSDATE  AND SASSSITEID = '" + SITE + "' " +
                                  " GROUP BY ITEMITEMID, ITEMITEMIDX " ;

                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAccessProfile Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetVariantByAssortment2(String Itemid, String SITE, String SITE2)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = " select VRNTVRNTID as VALUE, VRNTVRNTIDX as DESCRIPTION from KDSCMSSASS, KDSCMSMSTVRNT " +
                                  " where SASSITEMID = VRNTITEMID AND SASSVRNT = VRNTVRNTID " +
                                  " AND SASSCDAT <= SYSDATE  AND VRNTITEMID = '" + Itemid + "' " +
                                  " AND SASSSITEID  in ('" + SITE + "','" + SITE2 + "')  GROUP BY VRNTVRNTID, VRNTVRNTIDX ";

                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAccessProfile Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        public DataTable GetItemByAssortment2(String SITE, String SITE2)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select ITEMITEMID as VALUE, ITEMITEMIDX as DESCRIPTION from KDSCMSSASS, KDSCMSMSTITEM  " +
                                  " where SASSITEMID = ITEMITEMID AND SASSCDAT <= SYSDATE  AND SASSSITEID in ('" + SITE + "','" + SITE2 + "') " +
                                  " GROUP BY ITEMITEMID, ITEMITEMIDX ";

                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAccessProfile Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetSKULinkBoxBox(String SITE)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select SKUHSKUID as VALUE, SKUHSDES as DESCRIPTION " +
                                 "from KDSCMSSKUH  where SKUHEDAT >= CURRENT_DATE " +
                                 "AND SKUHSKUID IN (select distinct SKULINKSKUID from KDSCMSSKULINK WHERE " +
                                 " SKULINKSITEID = '" + SITE + "' )";

                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAccessProfile Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        public DataTable GetSKULinkBoxNoBrand(String SITE)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select SKUHSKUID as VALUE, SKUHSDES as DESCRIPTION " +
                                 "from KDSCMSSKUH  where SKUHEDAT >= CURRENT_DATE " +
                                 "AND SKUHSKUID IN (select distinct SKULINKSKUID from KDSCMSSKULINK WHERE " +
                                 " SKULINKSITEID = '" + SITE + "' )";

                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAccessProfile Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        public DataTable GetSKULinkBox(String SITE, String ITEMID)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select SKUHSKUID as VALUE, SKUHSDES as DESCRIPTION " +
                                 "from KDSCMSSKUH  where SKUHEDAT >= CURRENT_DATE " +
                                 "AND SKUHSKUID IN (select distinct SKULINKSKUID from KDSCMSSKULINK, KDSCMSMSTITEM WHERE " +
                                 " SKULINKSITEID = '" + SITE + "' and SKULINKBRNDID = ITEMBRNDID "+
                                 "  and ITEMITEMIDX =  '" + ITEMID + "' )";

                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAccessProfile Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
     

        public DataTable GetSKUBox()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select SKUHSKUID as VALUE, SKUHSDES as DESCRIPTION " +
                                 "from KDSCMSSKUH  where SKUHEDAT >= CURRENT_DATE ";

                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAccessProfile Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetBRANDBox()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select BRNDBRNDID as VALUE, BRNDDESC as DESCRIPTION " +
                                 "from KDSCMSMSTBRND";


                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAccessProfile Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        public DataTable GetSITEBox()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = " select SITESITE as VALUE, SITESITENAME as DESCRIPTION from KDSCMSSITE ";

                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAccessProfile Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        public DataTable GetVariant(string itemid)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = " select VRNTVRNTID as VALUE, VRNTVRNTIDX as DESCRIPTION from KDSCMSMSTVRNT where VRNTITEMID = '" + itemid + "' ";

                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAccessProfile Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        public OutputMessage deleteSKULinkHeader(SKULink skulink)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSKULINK.DEL_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PSKULINKSKUID", OracleDbType.Int32).Value = skulink.SKU;
                cmd.Parameters.Add("PSKULINKSITEID", OracleDbType.Varchar2, 50).Value = skulink.SITE;
                cmd.Parameters.Add("PSKULINKBRNDID", OracleDbType.Varchar2, 50).Value = skulink.BRAND;
                cmd.Parameters.Add("PSKULINKSDATE", OracleDbType.Date).Value = skulink.SDate.HasValue
                   ? skulink.SDate
                   : (object)DBNull.Value;
                cmd.Parameters.Add("PSKULINKEDATE", OracleDbType.Date).Value = skulink.EDate.HasValue
                   ? skulink.EDate
                   : (object)DBNull.Value;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public OutputMessage updateSKULinkHeader(SKULink skulink, String User)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSKULINK.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PSKULINKSKUID", OracleDbType.Int32).Value = skulink.SKU;
                cmd.Parameters.Add("PSKULINKSITEID", OracleDbType.Varchar2, 50).Value = skulink.SITE;
                cmd.Parameters.Add("PSKULINKBRNDID", OracleDbType.Varchar2, 50).Value = skulink.BRAND;
                cmd.Parameters.Add("PSKULINKSDATE", OracleDbType.Date).Value = skulink.SDate.HasValue
                   ? skulink.SDate
                   : (object)DBNull.Value;
                cmd.Parameters.Add("PSKULINKEDATE", OracleDbType.Date).Value = skulink.EDate.HasValue
                   ? skulink.EDate
                   : (object)DBNull.Value;
                cmd.Parameters.Add("PSKULINKINTF", OracleDbType.Varchar2, 50).Value = 1;
                cmd.Parameters.Add("PSKULINKMOBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage InsertSKULinkGroup(SKULink skulink, String User)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSKULINK.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PSKULINKSKUID", OracleDbType.Int32).Value = skulink.SKU;
                cmd.Parameters.Add("PSKULINKSITEID", OracleDbType.Varchar2, 50).Value = skulink.SITE;
                cmd.Parameters.Add("PSKULINKBRNDID", OracleDbType.Varchar2, 50).Value = skulink.BRAND;
                cmd.Parameters.Add("PSKULINKSDATE", OracleDbType.Date).Value = skulink.SDate.HasValue
                   ? skulink.SDate
                   : (object)DBNull.Value;
                cmd.Parameters.Add("PSKULINKEDATE", OracleDbType.Date).Value = skulink.EDate.HasValue
                   ? skulink.EDate
                   : (object)DBNull.Value;
                cmd.Parameters.Add("PSKULINKINTF", OracleDbType.Varchar2, 50).Value = 1;
                cmd.Parameters.Add("PSKULINKCRBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;

                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public DataTable GetSKULinkHeaderDataTable(SKULink skulink)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT SKULINKSKUID AS \"SKU ID\", " +
                                  "SKUHSDES AS \"SKU DESC\", " +
                                  "SKULINKSITEID AS \"SITE ID\", " +
                                  "SKULINKBRNDID AS \"BRAND ID\", " +
                                  "BRNDDESC AS \"BRAND DESC\", " +
                                  "SKULINKSDATE AS \"START DATE\", " +
                                  "SKULINKEDATE AS \"END DATE\", " +
                                  "SKULINKNMOD AS \"COUNTER MODIFICATION\" " +
                                  "FROM KDSCMSSKULINK, KDSCMSMSTBRND, KDSCMSSKUH " +
                                  "where BRNDBRNDID = SKULINKBRNDID and SKULINKSKUID =  SKUHSKUID ";


                if ((skulink.EDate.HasValue))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SKULINKEDATE <= :Edate  ";
                    cmd.Parameters.Add(new OracleParameter(":Edate", OracleDbType.Date)).Value = skulink.EDate;
                }
                if (skulink.SDate.HasValue)
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SKULINKSDATE >= :SDate  ";
                    cmd.Parameters.Add(new OracleParameter(":SDate", OracleDbType.Date)).Value = skulink.SDate;
                }


                if (!string.IsNullOrWhiteSpace(skulink.SKU))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SKULINKSKUID like '%' || :SKU || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":SKU", OracleDbType.Int32)).Value = skulink.SKU;
                }
                if (!string.IsNullOrWhiteSpace(skulink.SITE))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SKULINKSITEID like '%' || :SITE || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":SITE", OracleDbType.Varchar2)).Value = skulink.SITE;
                }
                if (!string.IsNullOrWhiteSpace(skulink.BRAND))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SKULINKBRNDID like '%' || :BRAND || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":BRAND", OracleDbType.Varchar2)).Value = skulink.BRAND;
                }

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("Get Price Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        //END SKULink
        
        //Price

        public OutputMessage updatePriceHeader(PriceGroup pricegroup, String User)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSPRICE.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PSPRCITEMID", OracleDbType.Varchar2, 10).Value = pricegroup.ItemID;
                cmd.Parameters.Add("PSPRCVRNTID", OracleDbType.Varchar2, 10).Value = pricegroup.VariantID;
                cmd.Parameters.Add("PSPRCSITE", OracleDbType.Varchar2, 20).Value = pricegroup.Site;
                cmd.Parameters.Add("PSPRCPRICE", OracleDbType.Int32).Value = pricegroup.Price;
                cmd.Parameters.Add("PSPRCVAT", OracleDbType.Int32).Value = pricegroup.VAT;
                cmd.Parameters.Add("PSPRCSDAT", OracleDbType.Date).Value = pricegroup.SDate.HasValue
                   ? pricegroup.SDate
                   : (object)DBNull.Value;

                cmd.Parameters.Add("PSPRCEDAT", OracleDbType.Date).Value = pricegroup.Edate.HasValue
                   ? pricegroup.Edate
                   : (object)DBNull.Value;

                cmd.Parameters.Add("PSPRCINTF", OracleDbType.Int32, 1).Value = 1;
                cmd.Parameters.Add("PSPRCMOBY", OracleDbType.Varchar2, 20).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;

                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage InsertPriceGroup(PriceGroup pricegroup, String User)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSPRICE.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PSPRCITEMID", OracleDbType.Varchar2, 50).Value = pricegroup.ItemID;
                cmd.Parameters.Add("PSPRCVRNTID", OracleDbType.Varchar2, 50).Value = pricegroup.VariantID;
                cmd.Parameters.Add("PSPRCSITE", OracleDbType.Varchar2, 50).Value = pricegroup.Site;
                cmd.Parameters.Add("PSPRCPRICE", OracleDbType.Int32, 50).Value = pricegroup.Price;
                cmd.Parameters.Add("PSPRCVAT", OracleDbType.Int32, 50).Value = pricegroup.VAT;

                cmd.Parameters.Add("PSPRCSDAT", OracleDbType.Date).Value = pricegroup.SDate.HasValue
                   ? pricegroup.SDate
                   : (object)DBNull.Value;

                cmd.Parameters.Add("PSPRCEDAT", OracleDbType.Date).Value = pricegroup.Edate.HasValue
                   ? pricegroup.Edate
                   : (object)DBNull.Value;

                cmd.Parameters.Add("PSPRCINTF", OracleDbType.Int32, 50).Value = 1;
                cmd.Parameters.Add("PSPRCCRBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;

                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public DataTable GetPriceHeaderDataTable(PriceGroup pricegroup)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                
                cmd.CommandText = "SELECT SPRCITEMID AS \"ITEM ID\", " +
                                  "SPRCVRNTID AS \"VARIANT ID\", " +
                                  "SPRCSITE AS \"SITE\", " +
                                  "SPRCPRICE AS \"PRICE\", " +
                                  "SPRCVAT AS \"VAT\", " +
                                  "SPRCSDAT AS \"START DATE\", " +
                                  "SPRCEDAT AS \"END DATE\", " +
                                  "SPRCNMOD AS \"COUNTER MODIFICATION\" " +
                                  "FROM KDSCMSSPRICE " +
                                  "where  SPRCITEMID is not null ";
               if ((pricegroup.Edate.HasValue))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SPRCEDAT <= :Edate  ";
                    cmd.Parameters.Add(new OracleParameter(":Edate", OracleDbType.Date)).Value = pricegroup.Edate;
                }
                if (pricegroup.SDate.HasValue)
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SPRCSDAT >= :SDate  ";
                    cmd.Parameters.Add(new OracleParameter(":SDate", OracleDbType.Date)).Value = pricegroup.SDate;
                }



                if (!string.IsNullOrWhiteSpace(pricegroup.ItemID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SPRCITEMID like '%' || :ItemID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":ItemID", OracleDbType.Varchar2)).Value = pricegroup.ItemID;
                }
                if (!string.IsNullOrWhiteSpace(pricegroup.VariantID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SPRCVRNTID like '%' || :VariantID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":VariantID", OracleDbType.Varchar2)).Value = pricegroup.VariantID;
                }
                if (!string.IsNullOrWhiteSpace(pricegroup.Site))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SPRCSITE like '%' || :Site || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":Site", OracleDbType.Varchar2)).Value = pricegroup.Site;
                }
                if (!string.IsNullOrWhiteSpace(pricegroup.Price))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SPRCPRICE like '%' || :Price || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":Price", OracleDbType.Varchar2)).Value = pricegroup.Price;
                }
                if (!string.IsNullOrWhiteSpace(pricegroup.VAT))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SPRCVAT like '%' || :VAT || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":VAT", OracleDbType.Varchar2)).Value = pricegroup.VAT;
                }
                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("Get Price Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }


        //End Price 

        //Search

        public DataTable GetSearchHeaderDataTable(SearchItemVariant search)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;





                cmd.CommandText = "SELECT a.VRNTITEMIDX AS \"ITEM ID\", " +
                                  "a.VRNTVRNTIDX AS \"VARIANT ID\", " +
                                  "c.BRCDBRCDID AS \"BARCODE\"," +
                                  "a.VRNTSDESC AS \"SHORT DESC\", " +
                                  "a.VRNTLDESC AS \"LONG DESC\", " +
                                  "b.DTLVRNTSZGID AS \"SIZE GROUP\", " +
                                  "b.DTLVRNTSZID AS \"SIZE ID\", " +
                                  "b.DTLVRNTCOGID AS \"COLOR GROUP\", " +
                                  "b.DTLVRNTCOLID AS \"COLOR ID\", " +
                                  "b.DTLVRNTSTGID AS \"STYLE GROUP\", " +
                                  "b.DTLVRNTSTYLID AS \"STYLE ID\" " +
                                  "FROM KDSCMSMSTBRCD c, KDSCMSMSTVRNT a left outer join KDSCMSDTLVRNT b on a.VRNTVRNTID = b.DTLVRNTVRNTID " +
                                  "where a.VRNTITEMID is not null and  c.BRCDITEMID = a.VRNTITEMID and c.BRCDVRNTID = a.VRNTVRNTID  ";

                if (!string.IsNullOrWhiteSpace(search.BARCODE))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and c.BRCDBRCDID like '%' || :BARCODE || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":BARCODE", OracleDbType.Varchar2)).Value = search.BARCODE;
                }
                if (!string.IsNullOrWhiteSpace(search.ITEMID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and a.VRNTITEMID like '%' || :ITEMID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":ITEMID", OracleDbType.Varchar2)).Value = search.ITEMID;
                }
                if (!string.IsNullOrWhiteSpace(search.SHORTDESC))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and a.VRNTSDESC like '%' || :SHORTDESC || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":SHORTDESC", OracleDbType.Varchar2)).Value = search.SHORTDESC;
                }
                if (!string.IsNullOrWhiteSpace(search.LONGDESC))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and a.VRNTLDESC like '%' || :LONGDESC || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":LONGDESC", OracleDbType.Varchar2)).Value = search.LONGDESC;
                }
                if (!string.IsNullOrWhiteSpace(search.VARIANTID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and a.a.VRNTVRNTID like '%' || :VARIANTID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":VARIANTID", OracleDbType.Varchar2)).Value = search.VARIANTID;
                }
                if (!string.IsNullOrWhiteSpace(search.SIZEGRP))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and b.DTLVRNTSZGID like '%' || :SIZEGRP || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":SIZEGRP", OracleDbType.Varchar2)).Value = search.SIZEGRP;
                }
                if (!string.IsNullOrWhiteSpace(search.SIZE))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and b.DTLVRNTSZID like '%' || :SIZE || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":SIZE", OracleDbType.Varchar2)).Value = search.SIZE;
                }
                if (!string.IsNullOrWhiteSpace(search.COLORGRP))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and b.DTLVRNTCOGID like '%' || :COLORGRP || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":COLORGRP", OracleDbType.Varchar2)).Value = search.COLORGRP;
                }
                if (!string.IsNullOrWhiteSpace(search.COLOR))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and b.DTLVRNTCOLID like '%' || :COLOR || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":COLOR", OracleDbType.Varchar2)).Value = search.COLOR;
                }
                if (!string.IsNullOrWhiteSpace(search.STYLEGRP))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and b.DTLVRNTSTGID like '%' || :STYLEGRP || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":STYLEGRP", OracleDbType.Varchar2)).Value = search.STYLEGRP;
                }
                if (!string.IsNullOrWhiteSpace(search.STYLE))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and b.DTLVRNTSTYLID like '%' || :STYLE || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":STYLE", OracleDbType.Varchar2)).Value = search.STYLE;
                }

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("Get Search Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        //End Search

        //SKU Master

        public DataTable GetTypeBox(String SiteClass, String ParameterID)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select pardetail.pardtabent as PARVALUE, Pardetail.Pardldesc as PARDESCRIPTION " +
                                  "from kdscmsparhtable parHeader inner join kdscmspardtable parDetail on Parheader.Parhtabid = Pardetail.Pardtabid " +
                                  "where parHeader.parhtabid = " + ParameterID + " " +
                                  "and parHeader.parhsclas = " + SiteClass + " " +
                                  "and  Parheader.Parhsclas = Pardetail.Pardsclas";

                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAccessProfile Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        public DataTable GetNameBox(String SiteClass, String ParameterID, String IDGRP)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select pardetail.PARDTABENT as PARVALUE, Pardetail.Pardldesc as PARDESCRIPTION " +
                                  "from kdscmsparhtable parHeader inner join kdscmspardtable parDetail on Parheader.Parhtabid = Pardetail.Pardtabid " +
                                  "where parHeader.parhtabid = " + ParameterID + " " +
                                  "and parHeader.parhsclas = " + SiteClass + " " +
                                  "and  Parheader.Parhsclas = Pardetail.Pardsclas " +
                                  "and parDetail.PARDTABENT not in (select SKUDNM from KDSCMSSKUD where SKUDSKUID = " + IDGRP + " ) ";

                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAccessProfile Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        public DataTable BaseBox(String IDGRP)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT 0 as BASE FROM KDSCMSPARDTABLE WHERE ROWNUM = 1 " +
                                  " union " +
                                  "select ROWNUM as BASE from KDSCMSSKUD a where SKUDSKUID = " + IDGRP + "  ";

                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetBase Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }


        public OutputMessage updateSKUHeader(SKUGroup skugroup, String User)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSKUH.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PSKUHSKUID", OracleDbType.Int32, 50).Value = skugroup.ID;
                cmd.Parameters.Add("PSKUHSKUIDX", OracleDbType.Varchar2, 50).Value = skugroup.EXID;
                cmd.Parameters.Add("PSKUHSDES", OracleDbType.Varchar2, 50).Value = skugroup.SDesc;
                cmd.Parameters.Add("PSKUHLDES", OracleDbType.Varchar2, 50).Value = skugroup.LDesc;
                cmd.Parameters.Add("PSKUHSDAT", OracleDbType.Date).Value = skugroup.SDate;
                cmd.Parameters.Add("PSKUHEDAT", OracleDbType.Date).Value = skugroup.EDate;
                cmd.Parameters.Add("PSKUHINTF", OracleDbType.Varchar2, 50).Value = 1;
                cmd.Parameters.Add("PSKUHMOBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage InsertSKUGroup(SKUGroup skugroup, String User)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                 

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSKUH.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PSKUHSKUID", OracleDbType.Int32, 50).Value = skugroup.ID;
                cmd.Parameters.Add("PSKUHSKUIDX", OracleDbType.Varchar2, 50).Value = skugroup.EXID;
                cmd.Parameters.Add("PSKUHSDES", OracleDbType.Varchar2, 50).Value = skugroup.SDesc;
                cmd.Parameters.Add("PSKUHLDES", OracleDbType.Varchar2, 50).Value = skugroup.LDesc;
                cmd.Parameters.Add("PSKUHSDAT", OracleDbType.Date).Value = skugroup.SDate;
                cmd.Parameters.Add("PSKUHEDAT", OracleDbType.Date).Value = skugroup.EDate;
                cmd.Parameters.Add("PSKUHINTF", OracleDbType.Varchar2, 50).Value = 1;
                cmd.Parameters.Add("PSKUHCRBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public DataTable GetSKUHeaderDataTable(SKUGroup skugroup)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;

                cmd.CommandText = "SELECT SKUHSKUID AS \"INTERNAL ID\", " +
                                  "SKUHSKUIDX AS \"EXTERNAL ID\", " +
                                  "SKUHSDES AS \"SHORT DESC\", " +
                                  "SKUHLDES AS \"LONG DESC\", " +
                                  "SKUHSDAT AS \"START DATE\", " +
                                  "SKUHEDAT AS \"END DATE\", " +
                                  "SKUHNMOD AS \"COUNTER MODIFICATION\" " +
                                  "FROM KDSCMSSKUH " +
                                  "where SKUHSKUID is not null ";

                if (!string.IsNullOrWhiteSpace(skugroup.ID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SKUHSKUID like '%' || :ID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":ID", OracleDbType.Varchar2)).Value = skugroup.ID;
                }
                if (!string.IsNullOrWhiteSpace(skugroup.EXID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SKUHSKUIDX like '%' || :EXID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":EXID", OracleDbType.Varchar2)).Value = skugroup.EXID;
                }
                if (!string.IsNullOrWhiteSpace(skugroup.SDesc))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SKUHSDES like '%' || :SDesc || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":SDesc", OracleDbType.Varchar2)).Value = skugroup.SDesc;
                }
                if (!string.IsNullOrWhiteSpace(skugroup.LDesc))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SKUHLDES like '%' || :LDesc || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":LDesc", OracleDbType.Varchar2)).Value = skugroup.LDesc;
                }
                if ((skugroup.EDate.HasValue))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SKUHEDAT <= :EDate  ";
                    cmd.Parameters.Add(new OracleParameter(":EDate", OracleDbType.Date)).Value = skugroup.EDate;
                }
                if (skugroup.SDate.HasValue)
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SKUHSDAT >= :SDate  ";
                    cmd.Parameters.Add(new OracleParameter(":SDate", OracleDbType.Date)).Value = skugroup.SDate;
                }

                
                cmd.CommandText = cmd.CommandText + " order by SKUHSKUID asc  ";
                
                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("Get Size Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public OutputMessage InsertSKUDetail(SKUGroupDetail skugroupdetail, String User)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSKUD.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PSKUDSKUID", OracleDbType.Int32).Value = skugroupdetail.IDGRP;
                cmd.Parameters.Add("PSKUDSKUIDD", OracleDbType.Int32).Value = skugroupdetail.ID;
                cmd.Parameters.Add("PSKUDSKUIDDX", OracleDbType.Varchar2, 50).Value = skugroupdetail.EXID;
                cmd.Parameters.Add("PSKUDNM", OracleDbType.Varchar2, 50).Value = skugroupdetail.NAME;
                cmd.Parameters.Add("PSKUDVAL", OracleDbType.Int32).Value = skugroupdetail.VALUE;
                cmd.Parameters.Add("PSKUDPART", OracleDbType.Int32).Value = skugroupdetail.PARTISIPASI;
                cmd.Parameters.Add("PSKUDBSON", OracleDbType.Int32).Value = skugroupdetail.BASEON;
                cmd.Parameters.Add("PSKUDTYPE", OracleDbType.Int32).Value = skugroupdetail.TYPE;
                cmd.Parameters.Add("PSTYLINTF", OracleDbType.Varchar2, 50).Value = 1;
                cmd.Parameters.Add("PSTYLCRBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();                
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage UpdateSKUDetail(SKUGroupDetail skugroupdetail, String User)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSKUD.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PSKUDSKUID", OracleDbType.Int32).Value = skugroupdetail.IDGRP;
                cmd.Parameters.Add("PSKUDSKUIDD", OracleDbType.Int32).Value = skugroupdetail.ID;
                cmd.Parameters.Add("PSKUDSKUIDDX", OracleDbType.Varchar2, 50).Value = skugroupdetail.EXID;
                cmd.Parameters.Add("PSKUDNM", OracleDbType.Varchar2, 50).Value = skugroupdetail.NAME;
                cmd.Parameters.Add("PSKUDVAL", OracleDbType.Int32).Value = skugroupdetail.VALUE;
                cmd.Parameters.Add("PSKUDPART", OracleDbType.Int32).Value = skugroupdetail.PARTISIPASI;
                cmd.Parameters.Add("PSKUDBSON", OracleDbType.Int32).Value = skugroupdetail.BASEON;
                cmd.Parameters.Add("PSKUDTYPE", OracleDbType.Int32).Value = skugroupdetail.TYPE;
                cmd.Parameters.Add("PSTYLINTF", OracleDbType.Varchar2, 50).Value = 1;
                cmd.Parameters.Add("PSTYLMOBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;




                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public DataTable GetSKUDetailDataTable(SKUGroupDetail skugroupdetail, String SKUID)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;


                cmd.CommandText = "SELECT SKUDSKUID AS \"GROUP ID\", " +
                                  "SKUDSKUIDD AS \"ID\", " +
                                  "SKUDSKUIDDX AS \"ID EXTERNAL\", " +
                                  "(select PARDLDESC FROM KDSCMSPARDTABLE where PARDTABENT = SKUDNM and PARDTABID = 17 and PARDSCLAS = 0 )AS \"NAME\", " +
                                  "SKUDLVL AS \"LEVEL\", " +
                                  "SKUDVAL AS \"VALUE\", " +
                                  "SKUDPART AS \"PARTICIPATION\", " +
                                  "SKUDBSON AS \"BASED ON\", " +
                                  "(select PARDLDESC FROM KDSCMSPARDTABLE WHERE PARDTABID = 7 and PARDSCLAS = 0 and PARDTABENT = SKUDTYPE) AS \"TYPE\", " +
                                  "SKUDNMOD AS \"COUNTER MODIFICATION\" " +
                                  "FROM KDSCMSSKUD " +
                                  "where SKUDSKUID = '" + SKUID + "' ";
                
                if (!string.IsNullOrWhiteSpace(skugroupdetail.IDGRP))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SKUDSKUID = :IDGRP  ";
                    cmd.Parameters.Add(new OracleParameter(":IDGRP", OracleDbType.Varchar2)).Value = skugroupdetail.IDGRP;
                }

                if (!string.IsNullOrWhiteSpace(skugroupdetail.ID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SKUDSKUIDD like '%' || :ID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":ID", OracleDbType.Varchar2)).Value = skugroupdetail.ID;
                }

                if (!string.IsNullOrWhiteSpace(skugroupdetail.EXID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SKUDSKUIDDX like '%' || :EXID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":EXID", OracleDbType.Varchar2)).Value = skugroupdetail.EXID;
                }

                if (!string.IsNullOrWhiteSpace(skugroupdetail.NAME))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SKUDNM like '%' || :NAME || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":NAME", OracleDbType.Varchar2)).Value = skugroupdetail.NAME;
                }
                if (!string.IsNullOrWhiteSpace(skugroupdetail.LEVEL))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SKUDLVL like '%' || :LEVEL || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":LEVEL", OracleDbType.Varchar2)).Value = skugroupdetail.LEVEL;
                }
                if (!string.IsNullOrWhiteSpace(skugroupdetail.VALUE))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SKUDVAL like '%' || :VALUE || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":VALUE", OracleDbType.Varchar2)).Value = skugroupdetail.VALUE;
                }
                if (!string.IsNullOrWhiteSpace(skugroupdetail.PARTISIPASI))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SKUDPART like '%' || :PARTISIPASI || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":PARTISIPASI", OracleDbType.Varchar2)).Value = skugroupdetail.PARTISIPASI;
                }
                if (!string.IsNullOrWhiteSpace(skugroupdetail.BASEON))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SKUDBSON like '%' || :BASEON || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":BASEON", OracleDbType.Varchar2)).Value = skugroupdetail.BASEON;
                }
                if (!string.IsNullOrWhiteSpace(skugroupdetail.TYPE))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SKUDTYPE like '%' || :TYPE || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":TYPE", OracleDbType.Varchar2)).Value = skugroupdetail.TYPE;
                }
                cmd.CommandText = cmd.CommandText +
                                      " order by SKUDSKUIDD asc";


                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSKUDetailData Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }




        public StyleGroupDetail GetSKUDetailUpdate(StyleGroupDetail stylegroupdetail)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;


                cmd.CommandText =
                                  " Select STYLSDES ," +
                                  "STYLLDES ," +
                                  "STYLORDR " +
                                  "FROM KDSCMSSTLTBL " +
                                  "where STYLSTYLID = " + stylegroupdetail.ID + "  and STYLSTGID = " + stylegroupdetail.GID + "    ";



                logger.Debug(cmd.CommandText);
                OracleDataReader dr = cmd.ExecuteReader();



                while (dr.Read())
                {

                    stylegroupdetail.StyleSDesc = dr["STYLSDES"].ToString();
                    stylegroupdetail.StyleLDesc = dr["STYLLDES"].ToString();
                    stylegroupdetail.StyleOrder = dr["STYLORDR"].ToString();

                }
                return stylegroupdetail;

            }
            catch (Exception e)
            {
                logger.Error("GetStyleDetailData Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        //END SKU Master

        //Path

        public string GetPath(int posisi)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;


                cmd.CommandText = "Select PARDVAC3 from KDSCMSPARDTABLE where PARDTABID = 15 and PARDSCLAS = 0 and PARDTABENT = " + posisi + " ";



                logger.Debug(cmd.CommandText);
                OracleDataReader dr = cmd.ExecuteReader();

                string path = "";

                while (dr.Read())
                {

                    path = dr["PARDVAC3"].ToString();


                }
                return path;

            }
            catch (Exception e)
            {
                logger.Error("GetPath Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        //End Path

        //Sales Input


        public OutputMessage updateSalesInputHeader(SalesHeader salesheader, String User)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSLSH.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PSLSHSLID", OracleDbType.Varchar2, 50).Value = salesheader.SALESID;
                cmd.Parameters.Add("PSLSHSLIDI", OracleDbType.Int32).Value = salesheader.IID;
                cmd.Parameters.Add("PSLSHSLNOTA", OracleDbType.Varchar2, 50).Value = salesheader.NOTA;
                cmd.Parameters.Add("PSLSHRCPTID", OracleDbType.Varchar2, 50).Value = salesheader.RECEIPTID;
                cmd.Parameters.Add("PSLSHSLDATE", OracleDbType.Date).Value = salesheader.DATE;
                cmd.Parameters.Add("PSLSHSITE", OracleDbType.Varchar2, 50).Value = salesheader.SITE;
                cmd.Parameters.Add("PSLSHCOMM", OracleDbType.Varchar2, 50).Value = salesheader.COMMENT;
                cmd.Parameters.Add("PSLSHINTF", OracleDbType.Int32).Value = 1;
                cmd.Parameters.Add("PSLSHMOBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public OutputMessage UpdateStatusSalesDetail(SalesInputDetail salesheader, String User, Int32 flag)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");


            try
            {
                if (flag == 5)
                {
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "PKKDSCMSSLSH.INS_DATA";
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.Add("PSLSHSLID", OracleDbType.Varchar2, 50).Value = "R" + salesheader.SALESID;
                    cmd.Parameters.Add("PSLSHSLIDI", OracleDbType.Int32).Value = salesheader.IID;
                    cmd.Parameters.Add("PSLSHSLNOTA", OracleDbType.Varchar2, 50).Value = salesheader.NOTA;
                    cmd.Parameters.Add("PSLSHRCPTID", OracleDbType.Varchar2, 50).Value = "R" + salesheader.RECEIPTID;
                    cmd.Parameters.Add("PSLSHSLDATE", OracleDbType.Date).Value = salesheader.DATE;
                    cmd.Parameters.Add("PSLSHSITE", OracleDbType.Varchar2, 50).Value = salesheader.SITE;
                    cmd.Parameters.Add("PSLSHVALBY", OracleDbType.Varchar2, 50).Value = salesheader.VALID;
                    cmd.Parameters.Add("PSLSHREJBY", OracleDbType.Varchar2, 50).Value = salesheader.REJECT;
                    cmd.Parameters.Add("PSLSHRETBY", OracleDbType.Varchar2, 50).Value = User;
                    cmd.Parameters.Add("PSLSHCOMM", OracleDbType.Varchar2, 50).Value = salesheader.COMMENT;
                    cmd.Parameters.Add("PSLSHSTAT", OracleDbType.Int32).Value = flag;
                    cmd.Parameters.Add("PSLSHFLAG", OracleDbType.Int32).Value = 1;
                    cmd.Parameters.Add("PSLSHINTF", OracleDbType.Int32).Value = 1;
                    cmd.Parameters.Add("PSLSHCRBY", OracleDbType.Varchar2, 50).Value = User;
                    cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                    logger.Debug("Execute Command");
                    logger.Debug(cmd.CommandText.ToString());

                    cmd.ExecuteNonQuery();
                    //OracleDataReader dr = cmd.ExecuteReader();
                    logger.Debug("End Execute Command");
                    outputMsg = new OutputMessage();

                    outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                    outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                    logger.Debug("Start Close Connection");
                }


                OracleCommand cmds = new OracleCommand();
                cmds.Connection = con;
                if (flag == 2) //Validate
                {
                    cmds.CommandText = "PKKDSCMSSLSD.UPD_DATA_VALIDATE";

                    cmds.CommandType = CommandType.StoredProcedure;
                }
                else if (flag == 3) //Reject 
                {
                    cmds.CommandText = "PKKDSCMSSLSD.UPD_DATA_REJECT";

                    cmds.CommandType = CommandType.StoredProcedure;
                }
                else if (flag == 4) //Delete 
                {
                    cmds.CommandText = "PKKDSCMSSLSD.UPD_DATA_STATUS_DELETE";

                    cmds.CommandType = CommandType.StoredProcedure;
                }
                else if (flag == 5) //Return
                {
                    cmds.CommandText = "PKKDSCMSSLSD.UPD_DATA_RETURN";

                    cmds.CommandType = CommandType.StoredProcedure;

                    cmds.Parameters.Add("RSLSHSLID", OracleDbType.Varchar2, 50).Value = "R" + salesheader.SALESID;
                    cmds.Parameters.Add("RSLSHRCPTID", OracleDbType.Varchar2, 50).Value = "R" + salesheader.RECEIPTID;
                }
                else if (flag == 1) //Confirm
                {
                    cmds.CommandText = "PKKDSCMSSLSD.UPD_DATA_STATUS_CONFIRM";
                    cmds.CommandType = CommandType.StoredProcedure;
                }
                cmds.Parameters.Add("PSLSHSLID", OracleDbType.Varchar2, 50).Value = salesheader.SALESID;
                cmds.Parameters.Add("PSLSHSLIDI", OracleDbType.Int32).Value = salesheader.IID;
                cmds.Parameters.Add("PSLSHSLNOTA", OracleDbType.Varchar2, 50).Value = salesheader.NOTA;
                cmds.Parameters.Add("PSLSHRCPTID", OracleDbType.Varchar2, 50).Value = salesheader.RECEIPTID;
                cmds.Parameters.Add("PSLSDLNNUM", OracleDbType.Int32).Value = salesheader.LINE;
                cmds.Parameters.Add("PSLSHSLDATE", OracleDbType.Date).Value = salesheader.DATE;
                cmds.Parameters.Add("PSLSHSITE", OracleDbType.Varchar2, 50).Value = salesheader.SITE;
                cmds.Parameters.Add("PSLSDCOMM", OracleDbType.Varchar2, 50).Value = "This Return Item";
                cmds.Parameters.Add("PSLSHSTAT", OracleDbType.Int32, 50).Value = flag;
                cmds.Parameters.Add("PSLSDFLAG", OracleDbType.Int32, 50).Value = flag;                
                cmds.Parameters.Add("PSLSHMOBY", OracleDbType.Varchar2, 50).Value = User;
                cmds.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmds.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmds.CommandText.ToString());

                cmds.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmds.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmds.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage InsertSalesInputHeader(SalesHeader salesheader, String User)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                if (salesheader.RECEIPTID == "Mobile")
                {
                    cmd.CommandText = "PKKDSCMSSLSH.INS_DATA_MOBILE";
                        }
                else
                {
                    cmd.CommandText = "PKKDSCMSSLSH.INS_DATA";
                }
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PSLSHSLID", OracleDbType.Varchar2, 50).Value = salesheader.SALESID;
                cmd.Parameters.Add("PSLSHSLIDI", OracleDbType.Int32).Value = salesheader.IID;
                cmd.Parameters.Add("PSLSHSLNOTA", OracleDbType.Varchar2, 50).Value = salesheader.NOTA;
                cmd.Parameters.Add("PSLSHRCPTID", OracleDbType.Varchar2, 50).Value = salesheader.RECEIPTID;
                cmd.Parameters.Add("PSLSHSLDATE", OracleDbType.Date).Value = salesheader.DATE;
                cmd.Parameters.Add("PSLSHSITE", OracleDbType.Varchar2, 50).Value = salesheader.SITE;
                cmd.Parameters.Add("PSLSHVALBY", OracleDbType.Varchar2, 50).Value = "";
                cmd.Parameters.Add("PSLSHREJBY", OracleDbType.Varchar2, 50).Value = "";
                cmd.Parameters.Add("PSLSHRETBY", OracleDbType.Varchar2, 50).Value = "";
                cmd.Parameters.Add("PSLSHCOMM", OracleDbType.Varchar2, 50).Value = salesheader.COMMENT;
                cmd.Parameters.Add("PSLSHSTAT", OracleDbType.Int32).Value = salesheader.STATUS;
                cmd.Parameters.Add("PSLSHFLAG", OracleDbType.Int32).Value = salesheader.FLAG;
                cmd.Parameters.Add("PSLSHINTF", OracleDbType.Int32).Value = 1;
                cmd.Parameters.Add("PSLSHCRBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public DataTable GetSalesHeaderDataTable(SalesHeader salesheader, Int32 Type)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT " +
                                  "SLSHSLNOTA AS \"NOTA\", " +
                                  "SLSHSLDATE AS \"DATE\", " +
                                  "SLSHSITE AS \"SITE\", " +
                                  "SLSHCOMM AS \"COMMENT\", " +
                                  "SLSHSLID AS \"TRANSACTION ID\", " +
                                  "SLSHSLIDI AS \"INTERNAL ID\", " +
                                  "SLSHRCPTID AS \"RECEIPT ID\", " +
                                  "(select PARDLDESC from KDSCMSPARDTABLE where PARDTABID = 8 and PARDSCLAS = 0 and PARDTABENT = SLSHSTAT) AS \"STATUS\" " +
                                  "FROM KDSCMSSLSH " +
                                  "where SLSHSTAT = " + Type + " ";
                if (!string.IsNullOrWhiteSpace(salesheader.SITE))
                {
                    cmd.CommandText = cmd.CommandText +
                                      " and SLSHSITE = :SITE2  ";
                    cmd.Parameters.Add(new OracleParameter(":SITE2", OracleDbType.Varchar2)).Value = salesheader.SITE;
                }


                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("Get Sales Input Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        public DataTable GetSalesInputHeaderDataTable(SalesHeader salesheader, String Type)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT a.SLSHSLID AS \"TRANSACTION ID\", " +
                                  "a.SLSHSLIDI AS \"INTERNAL ID\", " +
                                  "a.SLSHSLNOTA AS \"NOTA\", " +
                                  "a.SLSHRCPTID AS \"RECEIPT ID\", " +
                                  "a.SLSHSLDATE AS \"DATE\", " +
                                  "a.SLSHSITE AS \"SITE\", " +
                                  "a.SLSHCOMM AS \"COMMENT\", " +
                                  "(select PARDLDESC from KDSCMSPARDTABLE where PARDTABID = 8 and PARDSCLAS = 0 and PARDTABENT = a.SLSHSTAT) AS \"STATUS\", " +
                                  "(select PARDLDESC from KDSCMSPARDTABLE where PARDTABID = 9 and PARDSCLAS = 0 and PARDTABENT = a.SLSHFLAG ) AS \"FLAG\" " +
                                  "FROM KDSCMSSLSH a" +
                                  " where (SLSHSTAT in (" + Type + ") ) ";
                if (!string.IsNullOrWhiteSpace(salesheader.SITE))
                {
                    cmd.CommandText = cmd.CommandText +
                                      " and a.SLSHSITE = :SITE2  ";
                    cmd.Parameters.Add(new OracleParameter(":SITE2", OracleDbType.Varchar2)).Value = salesheader.SITE;
                }

                if (!string.IsNullOrWhiteSpace(salesheader.USER))
                {
                    cmd.CommandText = cmd.CommandText +
                                      " and a.SLSHCRBY = :USER2 ";
                    cmd.Parameters.Add(new OracleParameter(":USER2", OracleDbType.Varchar2)).Value = salesheader.USER;
                }

                    cmd.CommandText = cmd.CommandText + " UNION " +
                                  "SELECT a.SLSHSLID AS \"TRANSACTION ID\", " +
                                  "a.SLSHSLIDI AS \"INTERNAL ID\", " +
                                  "a.SLSHSLNOTA AS \"NOTA\", " +
                                  "a.SLSHRCPTID AS \"RECEIPT ID\", " +
                                  "a.SLSHSLDATE AS \"DATE\", " +
                                  "a.SLSHSITE AS \"SITE\", " +
                                  "a.SLSHCOMM AS \"COMMENT\", " +
                                  "(select PARDLDESC from KDSCMSPARDTABLE where PARDTABID = 8 and PARDSCLAS = 0 and PARDTABENT = a.SLSHSTAT) AS \"STATUS\", " +
                                  "(select PARDLDESC from KDSCMSPARDTABLE where PARDTABID = 9 and PARDSCLAS = 0 and PARDTABENT = a.SLSHFLAG ) AS \"FLAG\" " +
                                  "FROM KDSCMSSLSH a , KDSCMSSLSD b   where b.SLSDSTAT in (" + Type + ") and a.SLSHSLID = b.SLSDSLID and SLSHSLIDI = SLSDSLIDI  " +
                                  "and SLSHSLNOTA = SLSDSLNOTA and b.SLSDRCPTID = a.SLSHRCPTID ";

                if (!string.IsNullOrWhiteSpace(salesheader.SITE))
                {
                    cmd.CommandText = cmd.CommandText +
                                      " and a.SLSHSITE = :SITE1  ";
                    cmd.Parameters.Add(new OracleParameter(":SITE1", OracleDbType.Varchar2)).Value = salesheader.SITE;
                }

                if (!string.IsNullOrWhiteSpace(salesheader.USER))
                {
                    cmd.CommandText = cmd.CommandText +
                                      " and a.SLSHCRBY = :USER1  ";
                    cmd.Parameters.Add(new OracleParameter(":USER1", OracleDbType.Varchar2)).Value = salesheader.USER;
                }



                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("Get Sales Input Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        // Cek Data 

        public SalesInputDetail CekComment(SalesInputDetail salesinputdetail)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;


                cmd.CommandText =
                                  " select ROWNUM, 'NO' as Fail from KDSCMSSLSH " +
                                  " where rownum = 1 and SLSHSLID = '" + salesinputdetail.SALESID + "' " +
                                   " AND SLSHSLIDI =  '" + salesinputdetail.IID + "' " +
                                   " AND SLSHSLNOTA =  '" + salesinputdetail.NOTA + "' " +
                                   " AND SLSHRCPTID = '" + salesinputdetail.RECEIPTID + "' " +
                                   " AND (SLSHCOMM  = '' or SLSHCOMM is Null)";




                logger.Debug(cmd.CommandText);
                OracleDataReader dr = cmd.ExecuteReader();



                while (dr.Read())
                {

                    salesinputdetail.COMMENT = dr["Fail"].ToString();

                }
                return salesinputdetail;

            }
            catch (Exception e)
            {
                logger.Error("GetStyleDetailData Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public SalesInputDetail Cekdata(SalesInputDetail salesinputdetail)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;


                cmd.CommandText =
                                  " select ROWNUM, 'NO' as Fail from KDSCMSSLSD " +
                                  " where rownum = 1 and SLSDSLID = '" + salesinputdetail.SALESID + "' " +
                                   " AND SLSDSLIDI =  '" + salesinputdetail.IID + "' " +
                                   " AND SLSDSLNOTA =  '" + salesinputdetail.NOTA + "' " +
                                   " AND SLSDRCPTID = '" + salesinputdetail.RECEIPTID + "' " +
                                   " AND SLSDFLAG   in ( " + salesinputdetail.COMMENT + " )";




                logger.Debug(cmd.CommandText);
                OracleDataReader dr = cmd.ExecuteReader();



                while (dr.Read())
                {

                    salesinputdetail.COMMENT = dr["Fail"].ToString();

                }
                return salesinputdetail;

            }
            catch (Exception e)
            {
                logger.Error("GetStyleDetailData Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        public string TOProsesFlag(string transferid, string internalid, String User)
        {
            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSTRFH.UPD_DATA_STATUS_PROSES";
                
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add("PTRFDTRFID", OracleDbType.Varchar2, 50).Value = transferid;
                cmd.Parameters.Add("PTRFDTRFIDI", OracleDbType.Int32).Value = internalid;
                cmd.Parameters.Add("PTRFHINTF", OracleDbType.Int32).Value = 0;                
                cmd.Parameters.Add("PTRFDCRBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();
                string Results = "";
                Results = cmd.Parameters["POUTRSNCODE"].Value.ToString();
                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return Results;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        // End Cek
        public OutputMessage InsertSalesInputDetail(SalesInputDetail salesinputdetail, String User)
        {
            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSLSD.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PSLSDSLID", OracleDbType.Varchar2, 50).Value = salesinputdetail.SALESID;
                cmd.Parameters.Add("PSLSDSLIDI", OracleDbType.Int32).Value = salesinputdetail.IID;
                cmd.Parameters.Add("PSLSDSLNOTA", OracleDbType.Varchar2, 50).Value = salesinputdetail.NOTA;
                cmd.Parameters.Add("PSLSDRCPTID", OracleDbType.Varchar2, 50).Value = salesinputdetail.RECEIPTID;
                cmd.Parameters.Add("PSLSDSLDATE", OracleDbType.Date).Value = salesinputdetail.DATE;
                cmd.Parameters.Add("PSLSDSITE", OracleDbType.Varchar2, 50).Value = salesinputdetail.SITE;
                cmd.Parameters.Add("PSLSDITEMID", OracleDbType.Int32).Value = salesinputdetail.ITEMID;
                cmd.Parameters.Add("PSLSDVRNTID", OracleDbType.Int32).Value = salesinputdetail.VARIANTID;
                cmd.Parameters.Add("PSLSDBRCD", OracleDbType.Varchar2, 50).Value = salesinputdetail.BARCODE;
                cmd.Parameters.Add("PSLSDSLQTY", OracleDbType.Int32).Value = salesinputdetail.SALESQTY;
                cmd.Parameters.Add("PSLSDSKUID", OracleDbType.Varchar2, 50).Value = salesinputdetail.SKUID;
                cmd.Parameters.Add("PSLSDSLPRC", OracleDbType.Int32).Value = salesinputdetail.SALESPRICE;
                cmd.Parameters.Add("PSLSDCOMM", OracleDbType.Varchar2, 50).Value = salesinputdetail.COMMENT;
                cmd.Parameters.Add("PSLSDSTAT", OracleDbType.Int32).Value = 0;
                cmd.Parameters.Add("PSLSDFLAG", OracleDbType.Int32).Value = 1;      
                cmd.Parameters.Add("PSLSDINTF", OracleDbType.Varchar2, 50).Value = 0;
                cmd.Parameters.Add("PSLSDCRBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage UpdateSalesInputDetail(SalesInputDetail salesinputdetail, String User)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSLSD.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PSLSDSLID", OracleDbType.Varchar2, 50).Value = salesinputdetail.SALESID;
                cmd.Parameters.Add("PSLSDSLIDI", OracleDbType.Int32).Value = salesinputdetail.IID;
                cmd.Parameters.Add("PSLSDSLNOTA", OracleDbType.Varchar2, 50).Value = salesinputdetail.NOTA;
                cmd.Parameters.Add("PSLSDRCPTID", OracleDbType.Varchar2, 50).Value = salesinputdetail.RECEIPTID;
                cmd.Parameters.Add("PSLSDSLDATE", OracleDbType.Date).Value = salesinputdetail.DATE;
                cmd.Parameters.Add("PSLSDLNNUM", OracleDbType.Varchar2, 50).Value = salesinputdetail.LINE;
                cmd.Parameters.Add("PSLSDSITE", OracleDbType.Varchar2, 50).Value = salesinputdetail.SITE;
                cmd.Parameters.Add("PSLSDITEMID", OracleDbType.Int32).Value = salesinputdetail.ITEMID;
                cmd.Parameters.Add("PSLSDVRNTID", OracleDbType.Int32).Value = salesinputdetail.VARIANTID;
                cmd.Parameters.Add("PSLSDBRCD", OracleDbType.Varchar2, 50).Value = salesinputdetail.BARCODE;
                cmd.Parameters.Add("PSLSDSLQTY", OracleDbType.Int32).Value = salesinputdetail.SALESQTY;
                cmd.Parameters.Add("PSLSDSKUID", OracleDbType.Varchar2, 50).Value = salesinputdetail.SKUID;
                cmd.Parameters.Add("PSLSDSLPRC", OracleDbType.Int32).Value = salesinputdetail.SALESPRICE;
                cmd.Parameters.Add("PSLSDCOMM", OracleDbType.Varchar2, 50).Value = salesinputdetail.COMMENT;
                cmd.Parameters.Add("PSLSDSTAT", OracleDbType.Int32).Value = 0;
                cmd.Parameters.Add("PSLSDFLAG", OracleDbType.Int32).Value = 0;
                cmd.Parameters.Add("PSLSDINTF", OracleDbType.Varchar2, 50).Value = 1;
                cmd.Parameters.Add("PSLSDMOBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public DataTable GetSalesInputDetailReturnDataTable(SalesInputDetail salesinputdetail)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;

                cmd.CommandText = "SELECT SLSDLNNUM AS \"LINE\", " +
                                  "SLSDITEMID AS \"ITEM ID\", " +
                                  "SLSDVRNTID AS \"VARIANT ID\", " +
                                  "SLSDBRCD AS \"BARCODE\", " +
                                  "SLSDSLQTY AS \"QTY\", " +
                                  "SLSDSKUID AS \"SKU ID\", " +
                                  "SLSDSLPRC AS \"PRICE\", " +
                                  "SLSDTOTPRC AS \"TOTAL PRICE\", " +
                                  "SLSDDISCTOT AS \"DISCOUNT TOTAL\", " +
                                  "SLSDSLTOT AS \"SALES TOTAL\", " +
                                  "SLSDVALBY AS \"VALID BY\", " +
                                  "SLSDREJBY AS \"REJECT BY\" " +
                                  "FROM KDSCMSSLSD " +
                                  "where SLSDSTAT = '2' AND SLSDSLID = '" + salesinputdetail.SALESID + "' " +
                                  "and SLSDSLIDI = '" + salesinputdetail.IID + "' " +
                                  "and SLSDSLNOTA = '" + salesinputdetail.NOTA + "' " +
                                  "and SLSDRCPTID = '" + salesinputdetail.RECEIPTID + "' " +
                                  "and SLSDSITE = '" + salesinputdetail.SITE + "' ";


                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetStyleDetailData Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        public DataTable GetSalesInputDetailDataTable(SalesInputDetail salesinputdetail, String flag)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;

                cmd.CommandText = "SELECT SLSDLNNUM AS \"LINE\", " +
                                  "(select ITEMSDESC FROM KDSCMSMSTITEM WHERE ITEMITEMID = SLSDITEMID) AS \"ITEM ID\", " +
                                  "SLSDBRCD AS \"BARCODE\", " +
                                  "SLSDSLQTY AS \"QTY\", " +                                  
                                  "SLSDSLPRC AS \"PRICE\", " +
                                  "SLSDTOTPRC AS \"TOTAL PRICE\", " +
                                  "(SELECT SKUHLDES FROM KDSCMSSKUH where SKUHSKUID = SLSDSKUID) AS \"SKU ID\", " +
                                  "SLSDDISCTOT AS \"DISCOUNT TOTAL\", " +
                                  "SLSDSLTOTCUS AS \"SALES TOTAL\" " +
                                  "FROM KDSCMSSLSD " +
                                  "where SLSDSLID = '" + salesinputdetail.SALESID + "' " +
                                  "and SLSDSTAT in (" + flag + " ) " +
                                  "and SLSDSLIDI = '" + salesinputdetail.IID + "' " +
                                  "and SLSDSLNOTA = '" + salesinputdetail.NOTA + "' " +
                                  "and SLSDRCPTID = '" + salesinputdetail.RECEIPTID + "' " +
                                  "and SLSDSITE = '" + salesinputdetail.SITE + "' ";
                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetStyleDetailData Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public SalesInputDetail GetSalesInputDetailUpdate(SalesInputDetail salesinputdetail)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;


                cmd.CommandText =
                                  "select SKUHSDES, SLSDITEMID, SLSDVRNTID, SLSDBRCD, SLSDSLPRC, SLSDSLQTY, SLSDSKUID, SLSDCOMM from KDSCMSSLSD ,  KDSCMSSKUH  " +
                                  "where SLSDSKUID = SKUHSKUID and '" + salesinputdetail.RECEIPTID + "' = SLSDRCPTID and '" + salesinputdetail.SALESID + "' = SLSDSLID and '" + salesinputdetail.NOTA + "' = SLSDSLNOTA and '" + salesinputdetail.LINE + "' = SLSDLNNUM ";



                logger.Debug(cmd.CommandText);
                OracleDataReader dr = cmd.ExecuteReader();



                while (dr.Read())
                {

                    salesinputdetail.ITEMID = dr["SLSDITEMID"].ToString();
                    salesinputdetail.VARIANTID = dr["SLSDVRNTID"].ToString();
                    salesinputdetail.BARCODE = dr["SLSDBRCD"].ToString();
                    salesinputdetail.SALESPRICE = dr["SLSDSLPRC"].ToString();
                    salesinputdetail.SALESQTY = dr["SLSDSLQTY"].ToString();
                    salesinputdetail.SKUID = dr["SLSDSKUID"].ToString();
                    salesinputdetail.COMMENT = dr["SLSDCOMM"].ToString();
                    salesinputdetail.NOTA = dr["SKUHSDES"].ToString();

                }
                return salesinputdetail;

            }
            catch (Exception e)
            {
                logger.Error("GetStyleDetailData Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        public OutputMessage UpdateStatusSales(SalesHeader salesheader, String User, Int32 flag)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                if (flag == 2) //Validate
                {
                    cmd.CommandText = "PKKDSCMSSLSH.UPD_DATA_STATUS_VALIDATE";
                }
                else if (flag == 3) //Reject 
                {
                    cmd.CommandText = "PKKDSCMSSLSH.UPD_DATA_STATUS_REJECT";
                }
                else if (flag == 4) //Delete 
                {
                    cmd.CommandText = "PKKDSCMSSLSH.UPD_DATA_STATUS_DELETE";
                }
                else if (flag == 5) //Return
                {
                    cmd.CommandText = "PKKDSCMSSLSH.UPD_DATA_STATUS_RETURN";
                }
                else if (flag == 1) //Confirm
                {
                    cmd.CommandText = "PKKDSCMSSLSH.UPD_DATA_STATUS_CONFIRM";
                }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("PSLSHSLID", OracleDbType.Varchar2, 50).Value = salesheader.SALESID;
                cmd.Parameters.Add("PSLSHSLIDI", OracleDbType.Varchar2, 50).Value = salesheader.IID;
                cmd.Parameters.Add("PSLSHSLNOTA", OracleDbType.Varchar2, 50).Value = salesheader.NOTA;
                cmd.Parameters.Add("PSLSHRCPTID", OracleDbType.Varchar2, 50).Value = salesheader.RECEIPTID;
                cmd.Parameters.Add("PSLSHSLDATE", OracleDbType.Date).Value = salesheader.DATE;
                cmd.Parameters.Add("PSLSHSITE", OracleDbType.Varchar2, 50).Value = salesheader.SITE;
                cmd.Parameters.Add("PSLSHSTAT", OracleDbType.Int32, 50).Value = flag;
                cmd.Parameters.Add("PSLSHINTF", OracleDbType.Varchar2, 50).Value = 0;
                cmd.Parameters.Add("PSLSHMOBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public OutputMessage UpdateReturnStatusSales(SalesHeader salesheader, String User, Int32 flag)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSLSH.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PSLSHSLID", OracleDbType.Varchar2, 50).Value = "R" + salesheader.SALESID;
                cmd.Parameters.Add("PSLSHSLIDI", OracleDbType.Int32).Value = salesheader.IID;
                cmd.Parameters.Add("PSLSHSLNOTA", OracleDbType.Varchar2, 50).Value = salesheader.NOTA;
                cmd.Parameters.Add("PSLSHRCPTID", OracleDbType.Varchar2, 50).Value = "R" + salesheader.RECEIPTID;
                cmd.Parameters.Add("PSLSHSLDATE", OracleDbType.Date).Value = salesheader.DATE;
                cmd.Parameters.Add("PSLSHSITE", OracleDbType.Varchar2, 50).Value = salesheader.SITE;
                cmd.Parameters.Add("PSLSHVALBY", OracleDbType.Varchar2, 50).Value = salesheader.VALID;
                cmd.Parameters.Add("PSLSHREJBY", OracleDbType.Varchar2, 50).Value = salesheader.REJECT;
                cmd.Parameters.Add("PSLSHRETBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("PSLSHCOMM", OracleDbType.Varchar2, 50).Value = salesheader.COMMENT;
                cmd.Parameters.Add("PSLSHSTAT", OracleDbType.Int32).Value = 1;
                cmd.Parameters.Add("PSLSHFLAG", OracleDbType.Int32).Value = 2;
                cmd.Parameters.Add("PSLSHINTF", OracleDbType.Int32).Value = 1;
                cmd.Parameters.Add("PSLSHCRBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                //logger.Debug("Start Close Connection");

                OracleCommand cmds = new OracleCommand();
                cmds.Connection = con;
                cmds.CommandText = "PKKDSCMSSLSD.UPD_DATA_RETURN_WH";
                cmds.CommandType = CommandType.StoredProcedure;

                cmds.Parameters.Add("RSLSHSLID", OracleDbType.Varchar2, 50).Value = "R" + salesheader.SALESID;
                cmds.Parameters.Add("RSLSHRCPTID", OracleDbType.Varchar2, 50).Value = "R" + salesheader.RECEIPTID;
                cmds.Parameters.Add("PSLSHSLID", OracleDbType.Varchar2, 50).Value = salesheader.SALESID;
                cmds.Parameters.Add("PSLSHSLIDI", OracleDbType.Int32).Value = salesheader.IID;
                cmds.Parameters.Add("PSLSHSLNOTA", OracleDbType.Varchar2, 50).Value = salesheader.NOTA;
                cmds.Parameters.Add("PSLSHRCPTID", OracleDbType.Varchar2, 50).Value = salesheader.RECEIPTID;
                cmds.Parameters.Add("PSLSHSLDATE", OracleDbType.Date).Value = salesheader.DATE;
                cmds.Parameters.Add("PSLSHSITE", OracleDbType.Varchar2, 50).Value = salesheader.SITE;
                cmds.Parameters.Add("PSLSDCOMM", OracleDbType.Varchar2, 50).Value = "This Return Item";
                cmds.Parameters.Add("PSLSHSTAT", OracleDbType.Int32, 50).Value = 1;
                cmds.Parameters.Add("PSLSDFLAG", OracleDbType.Int32, 50).Value = 2;
                cmds.Parameters.Add("PSLSHMOBY", OracleDbType.Varchar2, 50).Value = User;
                cmds.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmds.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmds.CommandText.ToString());

                cmds.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmds.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmds.Parameters["POUTRSNMSG"].Value.ToString();

                OracleCommand cmdH = new OracleCommand();
                cmdH.Connection = con;
                cmdH.CommandText = "PKKDSCMSSLSH.UPD_DATA_STATUS_RETURN";
                cmdH.CommandType = CommandType.StoredProcedure;
                cmdH.Parameters.Add("PSLSHSLID", OracleDbType.Varchar2, 50).Value = salesheader.SALESID;
                cmdH.Parameters.Add("PSLSHSLIDI", OracleDbType.Varchar2, 50).Value = salesheader.IID;
                cmdH.Parameters.Add("PSLSHSLNOTA", OracleDbType.Varchar2, 50).Value = salesheader.NOTA;
                cmdH.Parameters.Add("PSLSHRCPTID", OracleDbType.Varchar2, 50).Value = salesheader.RECEIPTID;
                cmdH.Parameters.Add("PSLSHSLDATE", OracleDbType.Date).Value = salesheader.DATE;
                cmdH.Parameters.Add("PSLSHSITE", OracleDbType.Varchar2, 50).Value = salesheader.SITE;
                cmdH.Parameters.Add("PSLSHSTAT", OracleDbType.Int32, 50).Value = flag;
                cmdH.Parameters.Add("PSLSHINTF", OracleDbType.Varchar2, 50).Value = 1;
                cmdH.Parameters.Add("PSLSHMOBY", OracleDbType.Varchar2, 50).Value = User;
                cmdH.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmdH.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmdH.CommandText.ToString());

                cmdH.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmdH.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmdH.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        // End Sales Input
        
        //StyleGroup

        public OutputMessage updateStyleHeader(StyleGroup stylegroup, String User)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSTLGRP.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PSTGRSTGID", OracleDbType.Varchar2, 50).Value = stylegroup.ID;
                cmd.Parameters.Add("PSTGRDESC", OracleDbType.Varchar2, 50).Value = stylegroup.StyleDesc;
                cmd.Parameters.Add("PSTGRINTF", OracleDbType.Varchar2, 50).Value = 1;
                cmd.Parameters.Add("PSTGRMOBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage InsertStyleGroup(StyleGroup stylegroup, String User)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSTLGRP.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PSTGRSTGID", OracleDbType.Varchar2, 50).Value = stylegroup.ID;
                cmd.Parameters.Add("PSTGRDESC", OracleDbType.Varchar2, 50).Value = stylegroup.StyleDesc;
                cmd.Parameters.Add("PSTGRINTF", OracleDbType.Varchar2, 50).Value = 1;
                cmd.Parameters.Add("PSTGRCRBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public DataTable GetStyleHeaderDataTable(StyleGroup stylegroup)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT STGRSTGID AS \"STYLE GROUP ID\", " +
                                  "STGRDESC AS \"STYLE GROUP DESC\" " +
                                  "FROM KDSCMSSTLGRP " +
                                  "where STGRSTGID is not null ";

                if (!string.IsNullOrWhiteSpace(stylegroup.ID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and STGRSTGID like '%' || :ID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":ID", OracleDbType.Varchar2)).Value = stylegroup.ID;
                }

                if (!string.IsNullOrWhiteSpace(stylegroup.StyleDesc))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and STGRDESC like '%' || :DESCRIPTION || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":DESCRIPTION", OracleDbType.Varchar2)).Value = stylegroup.StyleDesc;
                }

                
                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("Get Size Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public OutputMessage InsertStyleDetail(StyleGroupDetail stylegroupdetail, String User)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSTLTBL.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add("PSTYLSTGID", OracleDbType.Varchar2, 50).Value = stylegroupdetail.GID;
                cmd.Parameters.Add("PSTYLSTYLID", OracleDbType.Varchar2, 50).Value = stylegroupdetail.ID;
                cmd.Parameters.Add("PSTYLSDES", OracleDbType.Varchar2, 50).Value = stylegroupdetail.StyleSDesc;
                cmd.Parameters.Add("PSTYLLDES", OracleDbType.Varchar2, 50).Value = stylegroupdetail.StyleLDesc;
                cmd.Parameters.Add("PSTYLORDR", OracleDbType.Int32, 50).Value = stylegroupdetail.StyleOrder;
                cmd.Parameters.Add("PSTYLINTF", OracleDbType.Varchar2, 50).Value = 1;
                cmd.Parameters.Add("PSTYLCRBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage UpdateStyleDetail(StyleGroupDetail stylegroupdetail, String User)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSTLTBL.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PSTYLSTGID", OracleDbType.Varchar2, 50).Value = stylegroupdetail.GID;
                cmd.Parameters.Add("PSTYLSTYLID", OracleDbType.Varchar2, 50).Value = stylegroupdetail.ID;
                cmd.Parameters.Add("PSTYLSDES", OracleDbType.Varchar2, 50).Value = stylegroupdetail.StyleSDesc;
                cmd.Parameters.Add("PSTYLLDES", OracleDbType.Varchar2, 50).Value = stylegroupdetail.StyleLDesc;
                cmd.Parameters.Add("PSTYLORDR", OracleDbType.Int32, 50).Value = stylegroupdetail.StyleOrder;
                cmd.Parameters.Add("PSTYLINTF", OracleDbType.Varchar2, 50).Value = 1;
                cmd.Parameters.Add("PSTYLMOBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;

                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public DataTable GetStyleDetailDataTable(StyleGroupDetail stylegroupdetail, String IDGRP)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;

                cmd.CommandText = "SELECT STYLSTGID AS \"STYLE ID\", " +
                                  "STYLSTYLID AS \"ID\", " +
                                  "STYLSDES AS \"STYLE SHORT DESC\", " +
                                  "STYLLDES AS \"STYLE LONG DESC\", " +
                                  "STYLORDR AS \"STYLE ORDER\", " +
                                  "STYLNMOD AS \"COUNTER MODIFICATION\" " +
                                  "FROM KDSCMSSTLTBL " +
                                  "where STYLSTGID is not null ";
                if (!string.IsNullOrWhiteSpace(IDGRP))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and STYLSTGID = :IDGRP  ";
                    cmd.Parameters.Add(new OracleParameter(":IDGRP", OracleDbType.Varchar2)).Value = IDGRP;
                }

                if (!string.IsNullOrWhiteSpace(stylegroupdetail.ID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and STYLSTYLID like '%' || :ID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":ID", OracleDbType.Varchar2)).Value = stylegroupdetail.ID;
                }

                if (!string.IsNullOrWhiteSpace(stylegroupdetail.StyleSDesc))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and STYLSDES like '%' || :StyleSDesc || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":StyleSDesc", OracleDbType.Varchar2)).Value = stylegroupdetail.StyleSDesc;
                }

                if (!string.IsNullOrWhiteSpace(stylegroupdetail.StyleLDesc))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and STYLLDES like '%' || :StyleLDesc || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":StyleLDesc", OracleDbType.Varchar2)).Value = stylegroupdetail.StyleLDesc;
                }
                if (!string.IsNullOrWhiteSpace(stylegroupdetail.StyleOrder))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and STYLORDR like '%' || :StyleOrder || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":StyleOrder", OracleDbType.Varchar2)).Value = stylegroupdetail.StyleOrder;
                }




                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetStyleDetailData Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public StyleGroupDetail GetStyleDetailUpdate(StyleGroupDetail stylegroupdetail)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;


                cmd.CommandText =
                                  " Select STYLSDES ," +
                                  "STYLLDES ," +
                                  "STYLORDR " +
                                  "FROM KDSCMSSTLTBL " +
                                  "where STYLSTYLID = '" + stylegroupdetail.ID + "'  and STYLSTGID = '" + stylegroupdetail.GID + "'    ";



                logger.Debug(cmd.CommandText);
                OracleDataReader dr = cmd.ExecuteReader();



                while (dr.Read())
                {

                    stylegroupdetail.StyleSDesc = dr["STYLSDES"].ToString();
                    stylegroupdetail.StyleLDesc = dr["STYLLDES"].ToString();
                    stylegroupdetail.StyleOrder = dr["STYLORDR"].ToString();

                }
                return stylegroupdetail;

            }
            catch (Exception e)
            {
                logger.Error("GetStyleDetailData Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        // End Style

        //TransferOrder Shipment

        public OutputMessage UpdateTOShipmentGroup(TransferOrderHeader transferorderheader, String User)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;

                cmd.CommandText = "PKKDSCMSTRFH.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add("PTRFHTRFID", OracleDbType.Varchar2, 50).Value = transferorderheader.ID;
                cmd.Parameters.Add("PTRFHTRDIDI", OracleDbType.Int32).Value = transferorderheader.IID;
                cmd.Parameters.Add("PTRFHTRFDATE", OracleDbType.Date).Value = transferorderheader.DATE;
                cmd.Parameters.Add("PTRFHTRFFR", OracleDbType.Varchar2, 50).Value = transferorderheader.FROM;
                cmd.Parameters.Add("PTRFHTRFTO", OracleDbType.Varchar2, 50).Value = transferorderheader.TO;
                cmd.Parameters.Add("PTRFHSTAT", OracleDbType.Int32).Value = transferorderheader.STATUS;
                cmd.Parameters.Add("PTRFHFLAG", OracleDbType.Int32).Value = transferorderheader.STATUS;
                cmd.Parameters.Add("PTRFHVALBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("PTRFHINTF", OracleDbType.Int32).Value = 1;
                cmd.Parameters.Add("PTRFHMOBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;




                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage InsertTOShipmentGroup(TransferOrderHeader transferorderheader, String User)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSTRFH.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add("PTRFHTRFID", OracleDbType.Varchar2, 50).Value = transferorderheader.ID;
                cmd.Parameters.Add("PTRFHTRDIDI", OracleDbType.Int32).Value = transferorderheader.IID;
                cmd.Parameters.Add("PTRFHTRFDATE", OracleDbType.Date).Value = transferorderheader.DATE;
                cmd.Parameters.Add("PTRFHTRFFR", OracleDbType.Varchar2, 50).Value = transferorderheader.FROM;
                cmd.Parameters.Add("PTRFHTRFTO", OracleDbType.Varchar2, 50).Value = transferorderheader.TO;
                cmd.Parameters.Add("PTRFHSTAT", OracleDbType.Int32).Value = transferorderheader.STATUS;
                cmd.Parameters.Add("PTRFHFLAG", OracleDbType.Int32).Value = transferorderheader.STATUS;
                cmd.Parameters.Add("PTRFHVALBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("PTRFHINTF", OracleDbType.Int32).Value = 1;
                cmd.Parameters.Add("PTRFHCRBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public DataTable GetTOCreationHeaderDataTable(TransferOrderHeader transferorderheader)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT a.TRFHTRFID AS \"TRANSFER ID\", " +
                                  "a.TRFHTRDIDI AS \"INTERNAL ID\", " +
                                  "a.TRFHTRFDATE AS \"TRANSFER DATE\", " +
                                  "a.TRFHTRFFR AS \"SITE FROM\", " +
                                  "a.TRFHTRFTO AS \"SITE TO\", " +
                                 // "(select b.PARDLDESC from  KDSCMSPARDTABLE b where b.PARDTABID = 10 and b.PARDTABENT = a.TRFHSTAT GROUP BY b.PARDLDESC) AS \"STATUS\", " +
                                  //"a.TRFHFLAG AS \"FLAG\", " +
                                  "a.TRFHVALBY AS \"CREATED USER\" " +
                                 // "a.TRFHNMOD AS \"COUNTER MODIFICATION\" " +
                                  "FROM KDSCMSTRFH a " +
                                  "where a.TRFHINTF = 9 ";
                if (!string.IsNullOrWhiteSpace(transferorderheader.ID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and a.TRFHTRFID like '%' || :ID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":ID", OracleDbType.Varchar2)).Value = transferorderheader.ID;
                }
                if (!string.IsNullOrWhiteSpace(transferorderheader.IID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and a.TRFHTRDIDI like '%' || :IID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":IID", OracleDbType.Int32)).Value = transferorderheader.IID;
                }
                if ((transferorderheader.DATE.HasValue))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and a.TRFHTRFDATE <= :DATE  ";
                    cmd.Parameters.Add(new OracleParameter(":DATE", OracleDbType.Date)).Value = transferorderheader.DATE;
                }
                if (!string.IsNullOrWhiteSpace(transferorderheader.FROM))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and a.TRFHTRFFR like '%' || :FROM || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":FROM", OracleDbType.Varchar2)).Value = transferorderheader.FROM;
                }
                if (!string.IsNullOrWhiteSpace(transferorderheader.TO))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and a.TRFHTRFTO like '%' || :TO || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":TO", OracleDbType.Varchar2)).Value = transferorderheader.TO;
                }
                if (!string.IsNullOrWhiteSpace(transferorderheader.STATUS))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and a.TRFHSTAT like '%' || :STATUS || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":STATUS", OracleDbType.Int32)).Value = transferorderheader.STATUS;
                }
                if (!string.IsNullOrWhiteSpace(transferorderheader.FLAG))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and a.TRFHFLAG like '%' || :FLAG || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":FLAG", OracleDbType.Int32)).Value = transferorderheader.FLAG;
                }
                if (!string.IsNullOrWhiteSpace(transferorderheader.VALIDATION))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and a.TRFHVALBY like '%' || :VALID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":VALID", OracleDbType.Varchar2)).Value = transferorderheader.VALIDATION;
                }
                
                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("Get Transfer Order Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        public DataTable GetTOShipmentHeaderDataTable(TransferOrderHeader transferorderheader, Int32 flag)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT a.TRFHTRFID AS \"TRANSFER ID\", " +
                                  "a.TRFHTRDIDI AS \"INTERNAL ID\", " +
                                  "a.TRFHTRFDATE AS \"TRANSFER DATE\", " +
                                  "a.TRFHTRFFR AS \"SITE FROM\", " +
                                  "a.TRFHTRFTO AS \"SITE TO\", " +
                                  "(select b.PARDLDESC from  KDSCMSPARDTABLE b where b.PARDTABID = 10 and b.PARDTABENT = a.TRFHSTAT GROUP BY b.PARDLDESC) AS \"STATUS\", " +
                                  "a.TRFHFLAG AS \"FLAG\", " +
                                  "a.TRFHVALBY AS \"CREATED USER\" " +
                                 // "a.TRFHNMOD AS \"COUNTER MODIFICATION\" " +
                                  "FROM KDSCMSTRFH a " +
                                  "where a.TRFHSTAT = " + flag + " and a.TRFHINTF != '9' ";
                if (!string.IsNullOrWhiteSpace(transferorderheader.ID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and a.TRFHTRFID like '%' || :ID1 || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":ID1", OracleDbType.Varchar2)).Value = transferorderheader.ID;
                }
                if (!string.IsNullOrWhiteSpace(transferorderheader.IID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and a.TRFHTRDIDI like '%' || :IID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":IID", OracleDbType.Int32)).Value = transferorderheader.IID;
                }
                if ((transferorderheader.DATE.HasValue))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and a.TRFHTRFDATE <= :DATE1  ";
                    cmd.Parameters.Add(new OracleParameter(":DATE1", OracleDbType.Date)).Value = transferorderheader.DATE;
                }
                if (!string.IsNullOrWhiteSpace(transferorderheader.FROM))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and a.TRFHTRFFR like '%' || :FROM1 || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":FROM1", OracleDbType.Varchar2)).Value = transferorderheader.FROM;
                }
                if (!string.IsNullOrWhiteSpace(transferorderheader.TO))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and a.TRFHTRFTO like '%' || :TO1 || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":TO1", OracleDbType.Varchar2)).Value = transferorderheader.TO;
                }
                if (!string.IsNullOrWhiteSpace(transferorderheader.STATUS))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and a.TRFHSTAT like '%' || :STATUS || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":STATUS", OracleDbType.Int32)).Value = transferorderheader.STATUS;
                }
                if (!string.IsNullOrWhiteSpace(transferorderheader.FLAG))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and a.TRFHFLAG like '%' || :FLAG || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":FLAG", OracleDbType.Int32)).Value = transferorderheader.FLAG;
                }
                if (!string.IsNullOrWhiteSpace(transferorderheader.VALIDATION))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and a.TRFHVALBY like '%' || :VALID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":VALID", OracleDbType.Varchar2)).Value = transferorderheader.VALIDATION;
                }
                //cmd.Parameters.Add(new OracleParameter(":ProfileId", OracleDbType.Varchar2)).Value = ProfileID;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("Get Transfer Order Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public TransferOrderHeader GetTOShipmentUpdate(TransferOrderHeader transferorderheader, String id)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;


                cmd.CommandText = "SELECT a.TRFHTRFID ,a.TRFHTRDIDI ,a.TRFHTRFDATE , a.TRFHTRFFR , a.TRFHTRFTO FROM KDSCMSTRFH a where a.TRFHTRFID = '" + id.ToString() + "'";



                logger.Debug(cmd.CommandText);
                OracleDataReader dr = cmd.ExecuteReader();



                while (dr.Read())
                {

                    transferorderheader.IID = dr["TRFHTRDIDI"].ToString();
                    transferorderheader.DATE = Convert.ToDateTime(dr["TRFHTRFDATE"].ToString());
                    transferorderheader.FROM = dr["TRFHTRFFR"].ToString();
                    transferorderheader.TO = dr["TRFHTRFTO"].ToString();

                }
                return transferorderheader;

            }
            catch (Exception e)
            {
                logger.Error("GetStyleDetailData Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public OutputMessage InsertTOShipmentDetail(TransferOrderDetail transferorderdetail, String User)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSTRFD.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add("PTRFDTRFID", OracleDbType.Varchar2, 50).Value = transferorderdetail.ID;
                cmd.Parameters.Add("PTRFDTRFIDI", OracleDbType.Int32).Value = transferorderdetail.IID;
                cmd.Parameters.Add("PTRFDITEMID", OracleDbType.Int32).Value = transferorderdetail.ITEMID;
                cmd.Parameters.Add("PTRFDVRNTID", OracleDbType.Int32).Value = transferorderdetail.VARIANT;
                cmd.Parameters.Add("PTRFDBRCD", OracleDbType.Varchar2, 50).Value = transferorderdetail.BARCODE;
                cmd.Parameters.Add("PTRFDQTY", OracleDbType.Int32).Value = transferorderdetail.QTY;
                cmd.Parameters.Add("PTRFDSHPQTY", OracleDbType.Int32).Value = 0;
                cmd.Parameters.Add("PTRFDRECQTY", OracleDbType.Int32).Value = 0;
                cmd.Parameters.Add("PTRFDSCRQTY", OracleDbType.Int32).Value = 0;
                cmd.Parameters.Add("PTRFDCOMM", OracleDbType.Varchar2, 50).Value = "";
                cmd.Parameters.Add("PTRFDSTAT", OracleDbType.Int32).Value = 0;
                cmd.Parameters.Add("PTRFDFLAG", OracleDbType.Int32).Value = 0;
                cmd.Parameters.Add("PTRFDINTF", OracleDbType.Varchar2, 50).Value = 1;
                cmd.Parameters.Add("PTRFDCRBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage UpdateTOShipmentDetail(TransferOrderDetail transferorderdetail, String User)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSTRFD.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add("PTRFDTRFID", OracleDbType.Varchar2, 50).Value = transferorderdetail.ID;
                cmd.Parameters.Add("PTRFDTRFIDI", OracleDbType.Int32).Value = transferorderdetail.IID;
                cmd.Parameters.Add("PTRFDITEMID", OracleDbType.Int32).Value = transferorderdetail.ITEMID;
                cmd.Parameters.Add("PTRFDVRNTID", OracleDbType.Int32).Value = transferorderdetail.VARIANT;
                cmd.Parameters.Add("PTRFDBRCD", OracleDbType.Varchar2, 50).Value = transferorderdetail.BARCODE;
                cmd.Parameters.Add("PTRFDQTY", OracleDbType.Int32).Value = transferorderdetail.QTY;
                cmd.Parameters.Add("PTRFDCOMM", OracleDbType.Varchar2, 50).Value = transferorderdetail.COMMENT;
                cmd.Parameters.Add("PTRFDINTF", OracleDbType.Varchar2, 50).Value = 1;
                cmd.Parameters.Add("PTRFDCRBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public OutputMessage UpdateShipmentDetail(TransferOrderDetail transferorderdetail, String User)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSTRFD.UPD_DATA_SHIPMENT";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add("PTRFDTRFID", OracleDbType.Varchar2, 50).Value = transferorderdetail.ID;
                cmd.Parameters.Add("PTRFDTRFIDI", OracleDbType.Int32).Value = transferorderdetail.IID;
                cmd.Parameters.Add("PTRFDITEMID", OracleDbType.Int32).Value = transferorderdetail.ITEMID;
                cmd.Parameters.Add("PTRFDVRNTID", OracleDbType.Int32).Value = transferorderdetail.VARIANT;
                cmd.Parameters.Add("PTRFDBRCD", OracleDbType.Varchar2, 50).Value = transferorderdetail.BARCODE;
                cmd.Parameters.Add("PTRFDQTY", OracleDbType.Int32).Value = transferorderdetail.QTY;
                cmd.Parameters.Add("PTRFDSHPQTY", OracleDbType.Int32).Value = transferorderdetail.SHIP;
                cmd.Parameters.Add("PTRFDCOMM", OracleDbType.Varchar2, 50).Value = transferorderdetail.COMMENT;
                cmd.Parameters.Add("PTRFDINTF", OracleDbType.Varchar2, 50).Value = 1;
                cmd.Parameters.Add("PTRFDCRBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public OutputMessage UpdateTOReceiveDetail(TransferOrderDetail transferorderdetail, String User)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSTRFD.UPD_DATA_RECEIVE";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add("PTRFDTRFID", OracleDbType.Varchar2, 50).Value = transferorderdetail.ID;
                cmd.Parameters.Add("PTRFDTRFIDI", OracleDbType.Int32).Value = transferorderdetail.IID;
                cmd.Parameters.Add("PTRFDITEMID", OracleDbType.Int32).Value = transferorderdetail.ITEMID;
                cmd.Parameters.Add("PTRFDVRNTID", OracleDbType.Int32).Value = transferorderdetail.VARIANT;
                cmd.Parameters.Add("PTRFDBRCD", OracleDbType.Varchar2, 50).Value = transferorderdetail.BARCODE;

                cmd.Parameters.Add("PTRFDSHPQTY", OracleDbType.Int32).Value = transferorderdetail.SHIP;
                cmd.Parameters.Add("PTRFDRECQTY", OracleDbType.Int32).Value = transferorderdetail.RECEIVE;
                cmd.Parameters.Add("PTRFDCOMM", OracleDbType.Varchar2, 50).Value = transferorderdetail.COMMENT;
                cmd.Parameters.Add("PTRFDSTAT", OracleDbType.Int32).Value = 0;
                cmd.Parameters.Add("PTRFDFLAG", OracleDbType.Int32).Value = 0;
                cmd.Parameters.Add("PTRFDINTF", OracleDbType.Varchar2, 50).Value = 1;
                cmd.Parameters.Add("PTRFDCRBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public DataTable GetTOShipmentDetailDataTable(TransferOrderDetail transferorderdetail)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;

                cmd.CommandText = "SELECT TRFDTRFID AS \"TRANSFER ID\", " +
                                  "TRFDTRFIDI AS \"INTERNAL ID\", " +
                                  "TRFDITEMID AS \"ITEM ID\", " +
                                  "TRFDVRNTID AS \"VARIANT ID\", " +
                                  "TRFDBRCD AS \"BARCODE\", " +
                                  "TRFDQTY AS \"QTY\", " +
                                  "TRFDSHPQTY AS \"SHIP\", " +
                                  "TRFDSCRQTY AS \"SCRAP\", " +
                                  "TRFDCOMM AS \"COMMENT\", " +
                                  "TRFDNMOD AS \"COUNTER MODIFICATION\" " +
                                  "FROM KDSCMSTRFD " +
                                  "where TRFDTRFID is not null ";

                if (!string.IsNullOrWhiteSpace(transferorderdetail.ID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDTRFID = :ID  ";
                    cmd.Parameters.Add(new OracleParameter(":ID", OracleDbType.Varchar2)).Value = transferorderdetail.ID;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.IID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDTRFIDI = :IID  ";
                    cmd.Parameters.Add(new OracleParameter(":IID", OracleDbType.Int32)).Value = transferorderdetail.IID;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.ITEMID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDITEMID = :ITEMID  ";
                    cmd.Parameters.Add(new OracleParameter(":ITEMID", OracleDbType.Int32)).Value = transferorderdetail.ITEMID;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.ITEMID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDVRNTID = :ITEMID  ";
                    cmd.Parameters.Add(new OracleParameter(":ITEMID", OracleDbType.Int32)).Value = transferorderdetail.VARIANT;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.BARCODE))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDBRCD = :BARCODE  ";
                    cmd.Parameters.Add(new OracleParameter(":BARCODE", OracleDbType.Varchar2)).Value = transferorderdetail.BARCODE;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.QTY))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDQTY = :QTY  ";
                    cmd.Parameters.Add(new OracleParameter(":QTY", OracleDbType.Int32)).Value = transferorderdetail.QTY;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.SHIP))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDSHPQTY = :SHIP  ";
                    cmd.Parameters.Add(new OracleParameter(":SHIP", OracleDbType.Int32)).Value = transferorderdetail.SHIP;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.RECEIVE))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDRECQTY = :RECEIVE  ";
                    cmd.Parameters.Add(new OracleParameter(":RECEIVE", OracleDbType.Int32)).Value = transferorderdetail.RECEIVE;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.SCRAP))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDSCRQTY = :SCRAP  ";
                    cmd.Parameters.Add(new OracleParameter(":SCRAP", OracleDbType.Int32)).Value = transferorderdetail.SCRAP;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.COMMENT))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDCOMM = :COMMENT  ";
                    cmd.Parameters.Add(new OracleParameter(":COMMENT", OracleDbType.Varchar2)).Value = transferorderdetail.COMMENT;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.STATUS))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDSTAT = :STATUS  ";
                    cmd.Parameters.Add(new OracleParameter(":STATUS", OracleDbType.Int32)).Value = transferorderdetail.STATUS;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.FLAG))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDFLAG = :FLAG  ";
                    cmd.Parameters.Add(new OracleParameter(":FLAG", OracleDbType.Int32)).Value = transferorderdetail.FLAG;
                }

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("Get TO Detail Data Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        public DataTable GetCreatedDetailDataTable(TransferOrderDetail transferorderdetail)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;

                cmd.CommandText = "SELECT TRFDTRFID AS \"TRANSFER ID\", " +
                                  "TRFDTRFIDI AS \"INTERNAL ID\", " +
                                  "TRFDITEMID AS \"ITEM ID\", " +
                                  "TRFDVRNTID AS \"VARIANT ID\", " +
                                  "TRFDBRCD AS \"BARCODE\", " +
                                  "TRFDQTY AS \"QTY\", " +
                                  "TRFDSHPQTY AS \"SHIP\", " +
                                  "TRFDSCRQTY AS \"SCRAP\", " +
                                  "TRFDCOMM AS \"COMMENT\", " +
                                  "TRFDNMOD AS \"COUNTER MODIFICATION\" " +
                                  "FROM KDSCMSTRFD " +
                                  "where TRFDTRFID is not null ";
                if (!string.IsNullOrWhiteSpace(transferorderdetail.ID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDTRFID = :ID  ";
                    cmd.Parameters.Add(new OracleParameter(":ID", OracleDbType.Varchar2)).Value = transferorderdetail.ID;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.IID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDTRFIDI = :IID  ";
                    cmd.Parameters.Add(new OracleParameter(":IID", OracleDbType.Int32)).Value = transferorderdetail.IID;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.ITEMID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDITEMID = :ITEMID  ";
                    cmd.Parameters.Add(new OracleParameter(":ITEMID", OracleDbType.Int32)).Value = transferorderdetail.ITEMID;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.ITEMID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDVRNTID = :ITEMID  ";
                    cmd.Parameters.Add(new OracleParameter(":ITEMID", OracleDbType.Int32)).Value = transferorderdetail.VARIANT;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.BARCODE))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDBRCD = :BARCODE  ";
                    cmd.Parameters.Add(new OracleParameter(":BARCODE", OracleDbType.Varchar2)).Value = transferorderdetail.BARCODE;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.QTY))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDQTY = :QTY  ";
                    cmd.Parameters.Add(new OracleParameter(":QTY", OracleDbType.Int32)).Value = transferorderdetail.QTY;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.SHIP))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDSHPQTY = :SHIP  ";
                    cmd.Parameters.Add(new OracleParameter(":SHIP", OracleDbType.Int32)).Value = transferorderdetail.SHIP;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.RECEIVE))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDRECQTY = :RECEIVE  ";
                    cmd.Parameters.Add(new OracleParameter(":RECEIVE", OracleDbType.Int32)).Value = transferorderdetail.RECEIVE;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.SCRAP))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDSCRQTY = :SCRAP  ";
                    cmd.Parameters.Add(new OracleParameter(":SCRAP", OracleDbType.Int32)).Value = transferorderdetail.SCRAP;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.COMMENT))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDCOMM = :COMMENT  ";
                    cmd.Parameters.Add(new OracleParameter(":COMMENT", OracleDbType.Varchar2)).Value = transferorderdetail.COMMENT;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.STATUS))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDSTAT = :STATUS  ";
                    cmd.Parameters.Add(new OracleParameter(":STATUS", OracleDbType.Int32)).Value = transferorderdetail.STATUS;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.FLAG))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDFLAG = :FLAG  ";
                    cmd.Parameters.Add(new OracleParameter(":FLAG", OracleDbType.Int32)).Value = transferorderdetail.FLAG;
                }

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("Get TO Detail Data Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public StyleGroupDetail GetTOShipmentDetailUpdate(StyleGroupDetail stylegroupdetail)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;


                cmd.CommandText =
                                  " Select STYLSDES ," +
                                  "STYLLDES ," +
                                  "STYLORDR " +
                                  "FROM KDSCMSSTLTBL " +
                                  "where STYLSTYLID = " + stylegroupdetail.ID + "  and STYLSTGID = " + stylegroupdetail.GID + "    ";



                logger.Debug(cmd.CommandText);
                OracleDataReader dr = cmd.ExecuteReader();



                while (dr.Read())
                {

                    stylegroupdetail.StyleSDesc = dr["STYLSDES"].ToString();
                    stylegroupdetail.StyleLDesc = dr["STYLLDES"].ToString();
                    stylegroupdetail.StyleOrder = dr["STYLORDR"].ToString();

                }
                return stylegroupdetail;

            }
            catch (Exception e)
            {
                logger.Error("GetStyleDetailData Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetTOReceiveDetailDataTable(TransferOrderDetail transferorderdetail)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;

                cmd.CommandText = "SELECT TRFDTRFID AS \"TRANSFER ID\", " +
                                  "TRFDTRFIDI AS \"INTERNAL ID\", " +
                                  "TRFDITEMID AS \"ITEM ID\", " +
                                  "TRFDVRNTID AS \"VARIANT ID\", " +
                                  "TRFDBRCD AS \"BARCODE\", " +
                                  "TRFDQTY AS \"QTY\", " +
                                  "TRFDSHPQTY AS \"SHIP\", " +
                                  "TRFDRECQTY AS \"RECEIVE\", " +
                                  "TRFDSCRQTY AS \"SCRAP\", " +
                                  "TRFDCOMM AS \"COMMENT\", " +
                                  "TRFDNMOD AS \"COUNTER MODIFICATION\" " +
                                  "FROM KDSCMSTRFD " +
                                  "where TRFDTRFID is not null ";
                
                if (!string.IsNullOrWhiteSpace(transferorderdetail.ID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDTRFID = :ID  ";
                    cmd.Parameters.Add(new OracleParameter(":ID", OracleDbType.Varchar2)).Value = transferorderdetail.ID;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.IID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDTRFIDI = :IID  ";
                    cmd.Parameters.Add(new OracleParameter(":IID", OracleDbType.Int32)).Value = transferorderdetail.IID;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.ITEMID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDITEMID = :ITEMID  ";
                    cmd.Parameters.Add(new OracleParameter(":ITEMID", OracleDbType.Int32)).Value = transferorderdetail.ITEMID;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.ITEMID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDVRNTID = :ITEMID  ";
                    cmd.Parameters.Add(new OracleParameter(":ITEMID", OracleDbType.Int32)).Value = transferorderdetail.VARIANT;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.BARCODE))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDBRCD = :BARCODE  ";
                    cmd.Parameters.Add(new OracleParameter(":BARCODE", OracleDbType.Varchar2)).Value = transferorderdetail.BARCODE;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.QTY))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDQTY = :QTY  ";
                    cmd.Parameters.Add(new OracleParameter(":QTY", OracleDbType.Int32)).Value = transferorderdetail.QTY;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.SHIP))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDSHPQTY = :SHIP  ";
                    cmd.Parameters.Add(new OracleParameter(":SHIP", OracleDbType.Int32)).Value = transferorderdetail.SHIP;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.RECEIVE))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDRECQTY = :RECEIVE  ";
                    cmd.Parameters.Add(new OracleParameter(":RECEIVE", OracleDbType.Int32)).Value = transferorderdetail.RECEIVE;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.SCRAP))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDSCRQTY = :SCRAP  ";
                    cmd.Parameters.Add(new OracleParameter(":SCRAP", OracleDbType.Int32)).Value = transferorderdetail.SCRAP;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.COMMENT))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDCOMM = :COMMENT  ";
                    cmd.Parameters.Add(new OracleParameter(":COMMENT", OracleDbType.Varchar2)).Value = transferorderdetail.COMMENT;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.STATUS))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDSTAT = :STATUS  ";
                    cmd.Parameters.Add(new OracleParameter(":STATUS", OracleDbType.Int32)).Value = transferorderdetail.STATUS;
                }
                if (!string.IsNullOrWhiteSpace(transferorderdetail.FLAG))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRFDFLAG = :FLAG  ";
                    cmd.Parameters.Add(new OracleParameter(":FLAG", OracleDbType.Int32)).Value = transferorderdetail.FLAG;
                }

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("Get TO Detail Data Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        public OutputMessage UpdateStatus(TransferOrderDetail transferorderdetail, String User, Int32 Stat)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                if (Stat == 2)
                {
                    cmd.CommandText = "PKKDSCMSTRFH.UPD_DATA_STATUS_SHIPING";
                }
                else
                {
                    cmd.CommandText = "PKKDSCMSTRFH.UPD_DATA_STATUS_PICKING";
                }
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add("PTRFDTRFID", OracleDbType.Varchar2, 50).Value = transferorderdetail.ID;
                cmd.Parameters.Add("PTRFDTRFIDI", OracleDbType.Int32).Value = transferorderdetail.IID;
                cmd.Parameters.Add("PTRFDSTAT", OracleDbType.Int32).Value = Stat;
                cmd.Parameters.Add("PTRFDFLAG", OracleDbType.Int32).Value = Stat;
                cmd.Parameters.Add("PTRFDCRBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }


        //End Shipment
        
        //SizeGroup

        public DataTable GetSizeHeaderDataTable(SizeGroup sizegroup)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT SZGRSZGID AS \"SIZE GROUP ID\", " +
                                  "SZGRDESC AS \"SIZE GROUP DESC\", " +
                                  "SZGRNMOD AS \"COUNTER MODIFICATION\" " +
                                  "FROM KDSCMSSIZEGRP " +
                                  "where SZGRSZGID is not null ";



                if (!string.IsNullOrWhiteSpace(sizegroup.SIZEID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SZGRSZGID like '%' || :ID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":ID", OracleDbType.Varchar2)).Value = sizegroup.SIZEID;
                }

                if (!string.IsNullOrWhiteSpace(sizegroup.SIZEDESC))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SZGRDESC like '%' || :DESCRIPTION || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":DESCRIPTION", OracleDbType.Varchar2)).Value = sizegroup.SIZEDESC;
                }

                
                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("Get Size Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetSizeDetailDataTable(SizeGroupDetail sizegroupdetail, String IDGRP)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;


                cmd.CommandText = "SELECT SIZESZGID AS \"GROUP ID\", " +
                                  "SIZESZID AS \"ID\", " +
                                  "SIZESDES AS \"SIZE SHORT DESC\", " +
                                  "SIZELDES AS \"SIZE LONG DESC\", " +
                                  "SIZEORDR AS \"SIZE ORDER\", " +
                                  "SIZENMOD AS \"COUNTER MODIFICATION\" " +
                                  "FROM KDSCMSSIZETBL " +
                                  "where SIZESZGID is not null ";

                if (!string.IsNullOrWhiteSpace(IDGRP))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SIZESZGID = :IDGRP  ";
                    cmd.Parameters.Add(new OracleParameter(":IDGRP", OracleDbType.Varchar2)).Value = IDGRP;
                }

                if (!string.IsNullOrWhiteSpace(sizegroupdetail.ID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SIZESZID like '%' || :ID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":ID", OracleDbType.Varchar2)).Value = sizegroupdetail.ID;
                }

                if (!string.IsNullOrWhiteSpace(sizegroupdetail.SizeSDesc))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SIZESDES like '%' || :SizeSDesc || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":SizeSDesc", OracleDbType.Varchar2)).Value = sizegroupdetail.SizeSDesc;
                }

                if (!string.IsNullOrWhiteSpace(sizegroupdetail.SizeLDesc))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SIZELDES like '%' || :SizeLDesc || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":SizeLDesc", OracleDbType.Varchar2)).Value = sizegroupdetail.SizeLDesc;
                }
                if (!string.IsNullOrWhiteSpace(sizegroupdetail.SizeOrder))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SIZEORDR like '%' || :SizeOrder || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":SizeOrder", OracleDbType.Varchar2)).Value = sizegroupdetail.SizeOrder;
                }




                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSizeDetailData Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public SizeGroupDetail GetSizeDetailUpdate(SizeGroupDetail sizegroupdetail)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;


                cmd.CommandText =
                                  " Select SIZESDES ," +
                                  "SIZELDES ," +
                                  "SIZEORDR " +
                                  "FROM KDSCMSSIZETBL " +
                                  "where SIZESZID = " + sizegroupdetail.ID + "  and SIZESZGID = " + sizegroupdetail.GID + "    ";



                logger.Debug(cmd.CommandText);
                OracleDataReader dr = cmd.ExecuteReader();



                while (dr.Read())
                {

                    sizegroupdetail.SizeSDesc = dr["SIZESDES"].ToString();
                    sizegroupdetail.SizeLDesc = dr["SIZELDES"].ToString();
                    sizegroupdetail.SizeOrder = dr["SIZEORDR"].ToString();

                }
                return sizegroupdetail;

            }
            catch (Exception e)
            {
                logger.Error("GetSizeDetailData Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }



        // End Size

        //Report

        public DataTable GetReportDataTable(String query)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select ROWNUM as No, a.* from ( " +
                                  query +
                                  ") a";

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        //Report End
        
        // Color Master
        public DataTable GetColorHeaderDataTable(ColorGroup colorgroup)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT COGRCOGID AS \"COLOR GROUP ID\", " +
                                  "COGRDESC AS \"COLOR GROUP DESC\", " +
                                  "COGRNMOD AS \"COUNTER MODIFICATION\" " +
                                  "FROM KDSCMSCOLGRP " +
                                  "where COGRCOGID is not null ";
                

                if (!string.IsNullOrWhiteSpace(colorgroup.Color))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and COGRDESC like '%' || :Name || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":Name", OracleDbType.Varchar2)).Value = colorgroup.Color;
                }

                if (!string.IsNullOrWhiteSpace(colorgroup.ID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and COGRCOGID like '%' || :ID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":ID", OracleDbType.Int32)).Value = colorgroup.ID;
                }

                
                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetColorDetailDataTable(ColorGroupDetail colorgroupdetail, String IDGRP)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;


                cmd.CommandText = "SELECT COLRCOGID AS \"GROUP ID\", " +
                                  "COLRCOLID AS \"ID\", " +
                                  "COLRSDES AS \"COLOR SHORT DESC\", " +
                                  "COLRLDES AS \"COLOR LONG DESC\", " +
                                  "COLRORDR AS \"COLOR ORDER\", " +
                                  "COLRNMOD AS \"COUNTER MODIFICATION\" " +
                                  "FROM KDSCMSCOLTBL " +
                                  "where COLRCOLID is not null ";

                if (!string.IsNullOrWhiteSpace(IDGRP))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and COLRCOGID = :IDGRP  ";
                    cmd.Parameters.Add(new OracleParameter(":IDGRP", OracleDbType.Varchar2)).Value = IDGRP;
                }

                if (!string.IsNullOrWhiteSpace(colorgroupdetail.ID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and COLRCOLID like '%' || :ID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":ID", OracleDbType.Varchar2)).Value = colorgroupdetail.ID;
                }

                if (!string.IsNullOrWhiteSpace(colorgroupdetail.ColorSDesc))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and COLRSDES like '%' || :ColorSDesc || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":ColorSDesc", OracleDbType.Varchar2)).Value = colorgroupdetail.ColorSDesc;
                }

                if (!string.IsNullOrWhiteSpace(colorgroupdetail.ColorLDesc))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and COLRLDES like '%' || :ColorLDesc || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":ColorLDesc", OracleDbType.Varchar2)).Value = colorgroupdetail.ColorLDesc;
                }
                if (!string.IsNullOrWhiteSpace(colorgroupdetail.ColorOrder))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and COLRORDR like '%' || :ColorOrder || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":ColorOrder", OracleDbType.Varchar2)).Value = colorgroupdetail.ColorOrder;
                }




                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetColorDetailData Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public ColorGroupDetail GetColorDetailUpdate(ColorGroupDetail colorgroupdetail)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;


                cmd.CommandText =
                                  " Select COLRSDES ," +
                                  "COLRLDES ," +
                                  "COLRORDR " +
                                  "FROM KDSCMSCOLTBL " +
                                  "where COLRCOLID = " + colorgroupdetail.ID + "  and COLRCOGID = " + colorgroupdetail.GID + "    ";



                logger.Debug(cmd.CommandText);
                OracleDataReader dr = cmd.ExecuteReader();



                while (dr.Read())
                {

                    colorgroupdetail.ColorSDesc = dr["COLRSDES"].ToString();
                    colorgroupdetail.ColorLDesc = dr["COLRLDES"].ToString();
                    colorgroupdetail.ColorOrder = dr["COLRORDR"].ToString();

                }
                return colorgroupdetail;

            }
            catch (Exception e)
            {
                logger.Error("GetColorDetailData Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        // End Color
        public DataTable GetParameterHeaderDataTable(ParameterHeader parHeader, String SiteClass)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT PARHTABID AS \"ID\", " +
                                  "PARHTABNM AS \"NAME\", " +
                                  "PARHSCLAS AS \"SITE CLASS\", " +
                                  "PARHCOPY AS COPY, " +
                                  "PARHTABCOM AS \"COMMENT\", " +
                                  "PARHTABLOCK AS \"BLOCK\" " +
                                  "FROM KDSCMSPARHTABLE " +
                                  "where PARHSCLAS = :SiteClass ";

                cmd.Parameters.Add(new OracleParameter(":SiteClass", OracleDbType.Int32)).Value = SiteClass;


                if (!string.IsNullOrWhiteSpace(parHeader.Name))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and parhtabnm like '%' || :Name || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":Name", OracleDbType.Varchar2)).Value = parHeader.Name;
                }

                if (!string.IsNullOrWhiteSpace(parHeader.ID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and PARHTABID like '%' || :ID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":ID", OracleDbType.Int32)).Value = parHeader.ID;
                }

                
                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetParameterDetailDataTable(String ID, String SiteClass)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "PARDTABID AS \"ID\", " +
                                  "PARDTABENT AS \"ENTRY\", " +
                                  "PARDSDESC AS \"SHORT DESCRIPTION\", " +
                                  "PARDLDESC AS \"LONG DESCRIPTION\", " +
                                  "PARDSCLAS AS \"SITE CLASS\", " +
                                  "PARDCOMM AS \"COMMENT\" " +
                                  "from kdscmspardtable " +
                                  "WHERE PARDTABID = :id " +
                                  "and pardsclas = :SiteClass";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":id", OracleDbType.Varchar2)).Value = ID;
                cmd.Parameters.Add(new OracleParameter(":SiteClass", OracleDbType.Varchar2)).Value = SiteClass;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetSiteProfileLinkDataTable(String SiteProfile)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "Prstsite as \"SITE\" , " +
                                  "prstsdat as \"START DATE\" , " +
                                  "prstedat as \"END DATE\"  " +
                                  "from Kdscmsprofsitelink " +
                                  "WHERE Prststprof = :SiteProfile ";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":SiteProfile", OracleDbType.Varchar2)).Value = SiteProfile;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetMenuProfileLinkDataTable(String MenuProfile)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "MenuProfile.MEPRDMEPROF as \"MENU PROFILE\", " +
                                  "MenuProfile.MEPRDMENUID as \"MENU ID\", " +
                                  "Menu.Menumenunm as \"MENU NAME\" " +
                                  "from KDSCMSMEPROFD MenuProfile inner join kdscmsmenu menu on Menuprofile.Meprdmenuid = Menu.Menumenuid " +
                                  "WHERE MEPRDMEPROF = :MenuProfile";

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":MenuProfile ", OracleDbType.Varchar2)).Value = MenuProfile;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetMenuDropDownMenuProfileFiltered(String MenuProfile)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select Menu.Menumenuid, " +
                                  "menu.menumenunm " +
                                  "from kdscmsmenu menu " +
                                  "where " +
                                  "exists(select 1 from Kdscmsmeprofd where  MEPRDMENUID = Menu.Menumenuid and Kdscmsmeprofd.Meprdmeprof  = :MenuProfile)";

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":MenuProfile ", OracleDbType.Varchar2)).Value = MenuProfile;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        public TransferOrderDetail GetPriceDetail(TransferOrderDetail transferorderdetail, String SITE)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select SPRCPRICE from KDSCMSSPRICE where '" + transferorderdetail.ITEMID + "' = SPRCITEMID and '" + transferorderdetail.VARIANT + "' = SPRCVRNTID and '" + SITE + "' LIKE  '%'||SPRCSITE||'%'  ";


                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();
                
                transferorderdetail.PRICE = "0";

                while (dr.Read())
                {

                    transferorderdetail.PRICE = dr["SPRCPRICE"].ToString();


                }

                this.Close();
                return transferorderdetail;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public TransferOrderDetail GetBarcodeTransferDetail2(TransferOrderDetail transferorderdetail, string SITE)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select BRCDBRCDID, BRCDITEMID, BRCDVRNTID from kdscmsMSTBRCD , KDSCMSSASS where " +
                                  " SASSITEMID = BRCDITEMID AND SASSVRNT = BRCDVRNTID AND BRCDBRCDID = '" +
                                  transferorderdetail.BARCODE + "' AND SASSSITEID = '" + SITE + "' group by BRCDBRCDID, BRCDITEMID, BRCDVRNTID ";


                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();



                transferorderdetail.BARCODE = "Not Found";
                transferorderdetail.ITEMID = "";
                transferorderdetail.VARIANT = "";

                while (dr.Read())
                {

                    transferorderdetail.BARCODE = dr["BRCDBRCDID"].ToString();
                    transferorderdetail.ITEMID = dr["BRCDITEMID"].ToString();
                    transferorderdetail.VARIANT = dr["BRCDVRNTID"].ToString();


                }

                this.Close();
                return transferorderdetail;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public TransferOrderDetail GetBarcodeTransferDetail3(TransferOrderDetail transferorderdetail, string SITE, string SITE2)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select BRCDBRCDID, BRCDITEMID, BRCDVRNTID from kdscmsMSTBRCD , KDSCMSSASS where " +
                                  " SASSITEMID = BRCDITEMID AND SASSVRNT = BRCDVRNTID AND BRCDBRCDID = '" +
                                  transferorderdetail.BARCODE + "' AND SASSSITEID in ('" + SITE + "', '" + SITE2 + "') group by BRCDBRCDID, BRCDITEMID, BRCDVRNTID ";


                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();



                transferorderdetail.BARCODE = "Not Found";
                transferorderdetail.ITEMID = "";
                transferorderdetail.VARIANT = "";

                while (dr.Read())
                {

                    transferorderdetail.BARCODE = dr["BRCDBRCDID"].ToString();
                    transferorderdetail.ITEMID = dr["BRCDITEMID"].ToString();
                    transferorderdetail.VARIANT = dr["BRCDVRNTID"].ToString();


                }

                this.Close();
                return transferorderdetail;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public TransferOrderDetail GetBarcodeTransferDetail(TransferOrderDetail transferorderdetail)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select BRCDBRCDID, BRCDITEMID, BRCDVRNTID from kdscmsMSTBRCD  WHERE BRCDBRCDID = '" +
                                  transferorderdetail.BARCODE + "' ";


                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();
                


                transferorderdetail.BARCODE = "Not Found";
                transferorderdetail.ITEMID = "";
                transferorderdetail.VARIANT = "";

                while (dr.Read())
                {

                    transferorderdetail.BARCODE = dr["BRCDBRCDID"].ToString();
                    transferorderdetail.ITEMID = dr["BRCDITEMID"].ToString();
                    transferorderdetail.VARIANT = dr["BRCDVRNTID"].ToString();


                }

                this.Close();
                return transferorderdetail;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public TransferOrderDetail GetBarcodeByItemVariant(TransferOrderDetail transferorderdetail)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select BRCDBRCDID, BRCDITEMID, BRCDVRNTID from kdscmsMSTBRCD  WHERE BRCDITEMID = '" +
                                  transferorderdetail.ITEMID + "' and BRCDVRNTID = '" +
                                  transferorderdetail.VARIANT + "' ";


                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();



                transferorderdetail.BARCODE = "Not Found";
                transferorderdetail.ITEMID = "";
                transferorderdetail.VARIANT = "";

                while (dr.Read())
                {

                    transferorderdetail.BARCODE = dr["BRCDBRCDID"].ToString();
                    transferorderdetail.ITEMID = dr["BRCDITEMID"].ToString();
                    transferorderdetail.VARIANT = dr["BRCDVRNTID"].ToString();


                }

                this.Close();
                return transferorderdetail;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public VariantDetail GetVariantDetail(String VariantID)
        {
            VariantDetail variantDetail = new VariantDetail();
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "DTLVRNTVRNTID, " +
                                  "DTLVRNTSZGID, " +
                                  "DTLVRNTSZID, " +
                                  "DTLVRNTCOGID, " +
                                  "DTLVRNTCOLID, " +
                                  "DTLVRNTSTGID, " +
                                  "DTLVRNTSTYLID " +
                                  "from Kdscmsdtlvrnt " +
                                  "where DTLVRNTVRNTID = :VariantID ";

                cmd.Parameters.Add(new OracleParameter(":VariantID", OracleDbType.Varchar2)).Value = VariantID;

                cmd.CommandType = CommandType.Text;




                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    variantDetail.VariantID = VariantID;
                    variantDetail.SizeGroup = dr["DTLVRNTSZGID"].ToString();
                    variantDetail.SizeDetail = dr["DTLVRNTSZID"].ToString();
                    variantDetail.ColorGroup = dr["DTLVRNTCOGID"].ToString();
                    variantDetail.ColorDetail = dr["DTLVRNTCOLID"].ToString();
                    variantDetail.StyleGroup = dr["DTLVRNTSTGID"].ToString();
                    variantDetail.StyleDetail = dr["DTLVRNTSTYLID"].ToString();
                }

                this.Close();
                return variantDetail;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public ParameterDetail GetParameterDetail(ParameterDetail paramDetail)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "PARDSDESC, " +
                                  "PARDLDESC, " +
                                  "PARDVAN1, " +
                                  "PARDVAN2, " +
                                  "PARDVAN3, " +
                                  "PARDVAN4, " +
                                  "PARDVAC1, " +
                                  "PARDVAC2, " +
                                  "PARDVAC3, " +
                                  "PARDDATE1, " +
                                  "PARDDATE2, " +
                                  "PARDDATE3, " +
                                  "PARDCOMM, " +
                                  "PARDCDAT, " +
                                  "PARDCDAT, " +
                                  "PARDCRBY, " +
                                  "PARDMOBY, " +
                                  "PARDNMOD " +
                                  "from kdscmspardtable " +
                                  "WHERE PARDTABID = :Id " +
                                  "AND PARDTABENT = :Entry " +
                                  "AND PARDSCLAS = :SiteClas ";


                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":id", OracleDbType.Int32)).Value = paramDetail.ID;
                cmd.Parameters.Add(new OracleParameter(":Entry", OracleDbType.Int32)).Value = paramDetail.Entry;
                cmd.Parameters.Add(new OracleParameter(":SiteClas", OracleDbType.Int32)).Value = paramDetail.SiteClass;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                DateTime Date;

                int Pardvan;

                while (dr.Read())
                {

                    paramDetail.ShortDescription = dr["PARDSDESC"].ToString();
                    paramDetail.LongDescription = dr["PARDLDESC"].ToString();


                    paramDetail.Number1 = Int32.TryParse(dr["PARDVAN1"].ToString(), out Pardvan)
                        ? (int?)Pardvan
                        : null;

                    paramDetail.Number2 = Int32.TryParse(dr["PARDVAN2"].ToString(), out Pardvan)
                       ? (int?)Pardvan
                       : null;

                    paramDetail.Number3 = Int32.TryParse(dr["PARDVAN3"].ToString(), out Pardvan)
                       ? (int?)Pardvan
                       : null;

                    paramDetail.Number4 = Int32.TryParse(dr["PARDVAN4"].ToString(), out Pardvan)
                       ? (int?)Pardvan
                       : null;

                    paramDetail.Char1 = dr["PARDVAC1"].ToString();
                    paramDetail.Char2 = dr["PARDVAC2"].ToString();
                    paramDetail.Char3 = dr["PARDVAC3"].ToString();

                    paramDetail.Date1 = DateTime.TryParse(dr["PARDDATE1"].ToString(), out Date)
                        ? (DateTime?)Convert.ToDateTime(dr["PARDDATE1"].ToString())
                        : null;

                    paramDetail.Date2 = DateTime.TryParse(dr["PARDDATE2"].ToString(), out Date)
                       ? (DateTime?)Convert.ToDateTime(dr["PARDDATE2"].ToString())
                       : null;

                    paramDetail.Date3 = DateTime.TryParse(dr["PARDDATE2"].ToString(), out Date)
                       ? (DateTime?)Convert.ToDateTime(dr["PARDDATE2"].ToString())
                       : null;

                    paramDetail.Comment = dr["PARDCOMM"].ToString();
                }

                this.Close();
                return paramDetail;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public SiteProfileLink GetSiteProfileLink(SiteProfileLink siteProfileLink)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "prstsite, " +
                                  "prstsdat, " +
                                  "prstedat " +
                                  "from Kdscmsprofsitelink " +
                                  "where prststprof = :SiteProfile " +
                                  "and prstsite = :Site";


                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":SiteProfile", OracleDbType.Varchar2)).Value = siteProfileLink.SiteProfile;
                cmd.Parameters.Add(new OracleParameter(":SiteProfile", OracleDbType.Varchar2)).Value = siteProfileLink.Site;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                DateTime Date1;

                while (dr.Read())
                {
                    siteProfileLink.Site = dr["prstsite"].ToString();
                    siteProfileLink.StartDate = DateTime.TryParse(dr["prstsdat"].ToString(), out Date1) ? Convert.ToDateTime(dr["prstsdat"].ToString()) : DateTime.MinValue;
                    siteProfileLink.EndDate = DateTime.TryParse(dr["prstedat"].ToString(), out Date1) ? Convert.ToDateTime(dr["prstedat"].ToString()) : DateTime.MinValue;
                }

                this.Close();
                return siteProfileLink;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public SiteProfileHeader GetSiteProfileDetail(SiteProfileHeader siteProfile)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select stprstdesc " +
                                  "from kdscmssiteprof " +
                                  "where stprstprof = :SiteProfile ";


                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":SiteProfile", OracleDbType.Varchar2)).Value = siteProfile.Profile;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();
                

                while (dr.Read())
                {
                    siteProfile.Description = dr["stprstdesc"].ToString();
                }

                this.Close();
                return siteProfile;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public MenuProfileHeader GetMenuProfileDetail(MenuProfileHeader menuProfile)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select MEPRHMEDESC " +
                                  "from kdscmsmeprofh " +
                                  "where MEPRHMEPROF = :MenuProfile ";


                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":MenuProfile", OracleDbType.Varchar2)).Value = menuProfile.Profile;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();
                

                while (dr.Read())
                {
                    menuProfile.Description = dr["MEPRHMEDESC"].ToString();
                }

                this.Close();
                return menuProfile;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public User GetUserFromUserId(String UserID)
        {
            User user = new User();

            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "USERUSNM, " +
                                  "USERPASW, " +
                                  "USERUSDSC, " +
                                  "USERSTAT, " +
                                  "USERSDAT, " +
                                  "USEREDAT, " +
                                  "USERTYPE, " +
                                  "USERIMEI, " +
                                  "USERACPROF, " +
                                  "USERMEPROF, " +
                                  "USERSTPROF " +
                                  "from kdscmsuser " +
                                  "where userusid = '" + UserID + "' ";

                cmd.CommandType = CommandType.Text;
                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    user.Username = dr["USERUSNM"].ToString();
                    user.Password = dr["USERPASW"].ToString();
                    user.Description = dr["USERUSDSC"].ToString();
                    user.Status = dr["USERSTAT"].ToString();
                    user.StartDate = DateTime.Parse(dr["USERSDAT"].ToString());
                    user.EndDate = DateTime.Parse(dr["USEREDAT"].ToString());
                    user.UserType = dr["USERTYPE"].ToString();
                    user.IMEI = dr["USERIMEI"].ToString();
                    user.AccessProfile = dr["USERACPROF"].ToString();
                    user.MenuProfile = dr["USERMEPROF"].ToString();
                    user.SiteProfile = dr["USERSTPROF"].ToString();
                }

                this.Close();
                return user;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public Menu GetMenuFromMenuID(String MenuID)
        {
            Menu menu = new Menu();

            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "MENUMENUNM , " +
                                  "MENUMEGRPID , " +
                                  "MENUMEGRPNM , " +
                                  "MENUMEURL " +
                                  "from kdscmsmenu " +
                                  "where MENUMENUID = :MenuID";

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":MenuID", OracleDbType.Int32)).Value = MenuID;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    menu.MenuName = dr["MENUMENUNM"].ToString();
                    menu.MenuGroupName = dr["MENUMEGRPNM"].ToString();
                    menu.MenuGroupID = dr["MENUMEGRPID"].ToString();
                    menu.MenuURL = dr["MENUMEURL"].ToString();
                }

                this.Close();
                return menu;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public SiteMaster GetSiteFromSiteCode(String siteCode)
        {
            SiteMaster siteMaster = new SiteMaster();
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "sitesite, " +
                                  "sitesclas, " +
                                  "sitesitename, " +
                                  "sitesiteflag " +
                                  "from kdscmssite " +
                                  "where sitesite = :Site";


                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":Site", OracleDbType.Varchar2)).Value = siteCode;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    siteMaster.Site = dr["sitesite"].ToString();
                    siteMaster.SiteClass = Int32.Parse(dr["sitesclas"].ToString());
                    siteMaster.SiteName = dr["sitesitename"].ToString();
                    siteMaster.Enable = Int32.Parse(dr["sitesiteflag"].ToString());
                }

                this.Close();
                return siteMaster;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public SiteMaster GetSiteIntFromRowID(String ROWID)
        {
            SiteMaster siteMaster = new SiteMaster();
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "INTSITESITE, " +
                                  "INTSITESCLAS, " +
                                  "INTSITESITENAME " +
                                  "from KDSCMSINTSITE " +
                                  "where ROWID = :ROWIDSITE";


                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":ROWIDSITE", OracleDbType.Varchar2)).Value = ROWID;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    siteMaster.Site = dr["INTSITESITE"].ToString();
                    siteMaster.SiteClass = Int32.Parse(dr["INTSITESCLAS"].ToString());
                    siteMaster.SiteName = dr["INTSITESITENAME"].ToString();
                    //siteMaster.Enable = Int32.Parse(dr["sitesiteflag"].ToString());
                }

                this.Close();
                return siteMaster;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public ItemMaster GetItemIntFromRowID(String ROWID)
        {
            ItemMaster item = new ItemMaster();
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT INTITEMITEMIDX, " +
                                  "INTITEMTYPE, " +
                                  "INTITEMSDESC, " +
                                  "INTITEMLDESC, " +
                                  "INTITEMBRNDID " +
                                  "FROM KDSCMSINTMSTITEM " +
                                  "where ROWID = :ROWIDITEM";


                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":ROWIDITEM", OracleDbType.Varchar2)).Value = ROWID;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    item.ItemIDExternal = dr["INTITEMITEMIDX"].ToString();
                    item.Type = dr["INTITEMTYPE"].ToString();
                    item.ShortDesc = dr["INTITEMSDESC"].ToString();
                    item.LongDesc = dr["INTITEMLDESC"].ToString();
                    item.Brand = dr["INTITEMBRNDID"].ToString();


                    //siteMaster.Enable = Int32.Parse(dr["sitesiteflag"].ToString());
                }

                this.Close();
                return item;
            }
            catch (Exception e)
            {
                logger.Error("GetItemIntFromRowID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public BarcodeMaster GetBarcodeIntFromRowID(String ROWID)
        {
            BarcodeMaster barcode = new BarcodeMaster();
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT INTBRCDBRCDID , " +
                                  "INTBRCDITEMID, " +
                                  "INTBRCDVRNTID, " +
                                  "INTBRCDTYPE, " +
                                  "INTBRCDSTAT, " +
                                  "INTBRCDSDAT, " +
                                  "INTBRCDEDAT " +
                                  "FROM KDSCMSINTMSTBRCD " +
                                  "where ROWID = :ROWIDBARCODE";


                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":ROWIDBARCODE", OracleDbType.Varchar2)).Value = ROWID;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    barcode.Barcode = dr["INTBRCDBRCDID"].ToString();
                    barcode.ItemID = dr["INTBRCDITEMID"].ToString();
                    barcode.VariantID = dr["INTBRCDVRNTID"].ToString();
                    barcode.Type = dr["INTBRCDTYPE"].ToString();
                    barcode.Status = dr["INTBRCDSTAT"].ToString();
                    barcode.StartDate = DateTime.Parse(dr["INTBRCDSDAT"].ToString());
                    barcode.EndDate = DateTime.Parse(dr["INTBRCDEDAT"].ToString());


                    //siteMaster.Enable = Int32.Parse(dr["sitesiteflag"].ToString());
                }

                this.Close();
                return barcode;
            }
            catch (Exception e)
            {
                logger.Error("GetBarcodeIntFromRowID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public AssortmentMaster GetAssortmentIntFromRowID(String ROWID)
        {
            AssortmentMaster assortment = new AssortmentMaster();
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT INTSASSITEMID, " +
                                  "INTSASSSITEID, " +
                                  "INTSASSVRNT, " +
                                  "INTSASSSTAT, " +
                                  "FROM KDSCMSINTSASS " +
                                  "where ROWID = :ROWIDASSORTMENT";


                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":ROWIDASSORTMENT", OracleDbType.Varchar2)).Value = ROWID;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    assortment.VariantID = dr["INTSASSVRNT"].ToString();
                    assortment.ItemID = dr["INTSASSITEMID"].ToString();
                    assortment.Site = dr["INTSASSSITEID"].ToString();
                    assortment.Status = dr["INTSASSSTAT"].ToString();


                    //siteMaster.Enable = Int32.Parse(dr["sitesiteflag"].ToString());
                }

                this.Close();
                return assortment;
            }
            catch (Exception e)
            {
                logger.Error("GetAssortmentIntFromRowID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public VariantDetail GetVariantIntFromRowID(String ROWID)
        {
            VariantDetail variantDetail = new VariantDetail();
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT INTDTLVRNTITMID, " +
                                  "INTDTLVRNTVRNTID, " +
                                  "INTDTLVRNTSZGID, " +
                                  "INTDTLVRNTSZID, " +
                                  "INTDTLVRNTCOGID, " +
                                  "INTDTLVRNTCOLID, " +
                                  "INTDTLVRNTSTGID, " +
                                  "INTDTLVRNTSTYLID " +
                                  "FROM KDSCMSINTDTLVRNT " +
                                  "where ROWID = :ROWIDVARIANT";


                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":ROWIDVARIANT", OracleDbType.Varchar2)).Value = ROWID;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    variantDetail.ItemID = dr["INTDTLVRNTITMID"].ToString();
                    variantDetail.VariantID = dr["INTDTLVRNTVRNTID"].ToString();
                    variantDetail.SizeGroup = dr["INTDTLVRNTSZGID"].ToString();
                    variantDetail.SizeDetail = dr["INTDTLVRNTSZID"].ToString();
                    variantDetail.ColorGroup = dr["INTDTLVRNTCOGID"].ToString();
                    variantDetail.ColorDetail = dr["INTDTLVRNTCOLID"].ToString();
                    variantDetail.StyleGroup = dr["INTDTLVRNTSTGID"].ToString();
                    variantDetail.StyleDetail = dr["INTDTLVRNTSTYLID"].ToString();
                    //siteMaster.Enable = Int32.Parse(dr["sitesiteflag"].ToString());
                }

                this.Close();
                return variantDetail;
            }
            catch (Exception e)
            {
                logger.Error("GetVariantIntFromRowID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DeliveryNote GetDNIntFromRowID(String ROWID)
        {

            DeliveryNote DN = new DeliveryNote();
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT " +
                                  "CMSID, " +
                                  "CMSITEMID, " +
                                  "CMSBARCODE, " +
                                  "CMSDESC, " +
                                  "CMSQTY, " +
                                  "CMSPRICE, " +
                                  "CMSSTORE, " +
                                  "CMSDATE," +
                                  "CMSUSERID  " +
                                  "FROM KDSCMSDN_INT " +
                                  "where ROWID = :ROWIDDN";


                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":ROWIDDN", OracleDbType.Varchar2)).Value = ROWID;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    DN.CMSId = dr["CMSID"].ToString();
                    DN.ItemID = dr["CMSITEMID"].ToString();
                    DN.Barcode= dr["CMSBARCODE"].ToString();
                    DN.Description = dr["CMSDESC"].ToString();
                    DN.Qty = Int32.Parse(dr["CMSQTY"].ToString());
                    DN.Price = decimal.Parse(dr["CMSPRICE"].ToString());
                    DN.Store = dr["CMSSTORE"].ToString();
                    DN.Date = DateTime.Parse(dr["CMSDATE"].ToString());
                    DN.UserID = dr["CMSUSERID"].ToString();
                    //siteMaster.Enable = Int32.Parse(dr["sitesiteflag"].ToString());
                }

                this.Close();
                return DN;
            }
            catch (Exception e)
            {
                logger.Error("GetVariantIntFromRowID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public PriceGroup GetPriceAssortmentIntFromRowID(String ROWID)
        {
            PriceGroup priceGroup = new PriceGroup();
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT INTSPRCITEMIDX, " +
                                  "INTSPRCDVRNTIDX, " +
                                  "INTSPRCSITE, " +
                                  "INTSPRCSPRICE, " +
                                  "INTSPRCVAT, " +
                                  "INTSPRCSDAT, " +
                                  "INTSPRCEDAT," +
                                  "INTASSSTAT " +
                                  "FROM KDSCMSINTSPRICEASS " +
                                  "where ROWID = :ROWIDPRICE";


                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":ROWIDPRICE", OracleDbType.Varchar2)).Value = ROWID;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    priceGroup.ItemID = dr["INTSPRCITEMIDX"].ToString();
                    priceGroup.VariantID = dr["INTSPRCDVRNTIDX"].ToString();
                    priceGroup.Site = dr["INTSPRCSITE"].ToString();
                    priceGroup.Price = dr["INTSPRCSPRICE"].ToString();
                    priceGroup.VAT = dr["INTSPRCVAT"].ToString();
                    priceGroup.SDate = DateTime.Parse(dr["INTSPRCSDAT"].ToString());
                    priceGroup.Edate = DateTime.Parse(dr["INTSPRCEDAT"].ToString());
                    priceGroup.AssortmentStatus = dr["INTASSSTAT"].ToString();

                    //siteMaster.Enable = Int32.Parse(dr["sitesiteflag"].ToString());
                }

                this.Close();
                return priceGroup;
            }
            catch (Exception e)
            {
                logger.Error("GetPriceAssortmentIntFromRowID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetMenuDataByProfileID(String ProfileID)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT MENUID, MENUNAME FROM KDSMENUCMSV3 WHERE MENUID IN( SELECT MENUID FROM KDSPROFILEMENULINKCMSV3 WHERE PROFILEID = :ProfileId )";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":ProfileId", OracleDbType.Varchar2)).Value = ProfileID;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetMenuDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }


        public DataTable GetAllSiteDataTable()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "sitesite as \"SITE\", " +
                                  "sitesclas AS \"SITECLASS\", " +
                                  "sitesitename as \"Site NAME\" " +
                                  "from kdscmssite";


                cmd.CommandType = CommandType.Text;


                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllSiteComboBox()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select site.SITESITE, " +
                                  "site.SITESITE || ' ' || site.SITESITENAME as SITESITENAME " +
                                  "from kdscmssite site ";


                cmd.CommandType = CommandType.Text;


                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllUserDataTable()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT USERCMS.USERUSID AS \"USER ID\", " +
                                  "USERCMS.USERUSNM AS \"USER NAME\", " +
                                  "USERCMS.USERPASW AS PASSWORD, " +
                                  "USERCMS.USERUSDSC AS DESCRIPTION, " +
                                  "DECODE (USERCMS.USERSTAT, " +
                                  "1, 'Active', " +
                                  "2, 'Frozen', " +
                                  "3, 'Delete', " +
                                  "'Unknown Code') AS STATUS, " +
                                  "USERCMS.USERSDAT AS \"START DATE\", " +
                                  "USERCMS.USEREDAT AS \"END DATE\", " +
                                  "DECODE (USERCMS.USERTYPE, " +
                                  "0, 'Web'," +
                                  "1, 'Android'," +
                                  "'Unknown Code' ) AS TYPE, " +
                                  "USERCMS.USERIMEI AS IMEI, " +
                                  "USERCMS.USERACPROF AS \"ACCESS PROFILE\", " +
                                  "USERCMS.USERMEPROF AS \"MENU PROFILE\", " +
                                  "USERCMS.USERSTPROF AS \"SITE PROFILE\" " +
                                  "FROM KDSCMSUSER USERCMS " +
                                  "WHERE USERCMS.USERUSID IS NOT NULL";


                cmd.CommandType = CommandType.Text;

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllMenuDataTable()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select MENUMENUID as \"ID\", " +
                                  "MENUMENUNM as \"MENU NAME\", " +
                                  "MENUMEGRPID as \"MENU GROUP ID\", " +
                                  "MENUMEGRPNM as \"MENU GROUP NAME\", " +
                                  "MENUMEURL as \"MENU URL\" " +
                                  "from kdscmsmenu";


                cmd.CommandType = CommandType.Text;


                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllSiteProfileDataTable()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select Stprstprof as \"SITE PROFILE\", Stprstdesc as \"PROFILE DESCRIPTION\" from Kdscmssiteprof";


                cmd.CommandType = CommandType.Text;

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllMenuProfileDataTable()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select Meprhmeprof AS  \"MENU PROFILE\", Meprhmedesc AS \"DESCRIPTION\" from kdscmsmeprofh";

                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public AccessProfileHeader GetAccessProfileDetail(AccessProfileHeader accessProfile)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select ACPRHACDESC " +
                                  "from Kdscmsaccprofh " +
                                  "where ACPRHACPROF = :AccessProfile";


                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":AccessProfile", OracleDbType.Varchar2)).Value = accessProfile.Profile;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();
                

                while (dr.Read())
                {
                    accessProfile.Description = dr["ACPRHACDESC"].ToString();
                }

                this.Close();
                return accessProfile;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllAccessProfileDataTable()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "ACPRHACPROF as \"ACCESS PROFILE\", " +
                                  "ACPRHACDESC as \"DESCRIPTION\"  " +
                                  "from Kdscmsaccprofh ";


                cmd.CommandType = CommandType.Text;



                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllSiteClass()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT DISTINCT SITESCLAS AS SITECLASS FROM KDSCMSSITE ORDER BY SITECLASS";
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAllSiteClass Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllUserFiltered(User user)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT USERCMS.USERUSID AS \"USER ID\", " +
                                  "USERCMS.USERUSNM AS \"USER NAME\", " +
                                  "USERCMS.USERUSDSC AS DESCRIPTION, " +
                                  "DECODE (USERCMS.USERSTAT, " +
                                  "1, 'Active', " +
                                  "2, 'Frozen', " +
                                  "3, 'Delete', " +
                                  "'Unknown Code') AS STATUS, " +
                                  "USERCMS.USERSDAT AS \"START DATE\", " +
                                  "USERCMS.USEREDAT AS \"END DATE\", " +
                                  "DECODE (USERCMS.USERTYPE, " +
                                  "0, 'Web'," +
                                  "1, 'Android'," +
                                  "'Unknown Code' ) AS TYPE, " +
                                  "USERCMS.USERIMEI AS IMEI, " +
                                  "USERCMS.USERACPROF AS \"ACCESS PROFILE\", " +
                                  "USERCMS.USERMEPROF AS \"MENU PROFILE\", " +
                                  "USERCMS.USERSTPROF AS \"SITE PROFILE\" " +
                                  "FROM KDSCMSUSER USERCMS " +
                                  "WHERE USERCMS.USERUSID IS NOT NULL ";


                cmd.CommandType = CommandType.Text;

                if (!string.IsNullOrWhiteSpace(user.UserID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and USERCMS.USERUSID like '%' || :ID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":ID", OracleDbType.Varchar2)).Value = user.UserID;
                }

                if (!string.IsNullOrWhiteSpace(user.Username))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and USERCMS.USERUSNM like '%' || :UserName || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":UserName", OracleDbType.Varchar2)).Value = user.Username;
                }

                if (!string.IsNullOrWhiteSpace(user.Password))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and USERCMS.USERPASW like '%' || :Password || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":Password", OracleDbType.Varchar2)).Value = user.Password;
                }

                if (!string.IsNullOrWhiteSpace(user.Description))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and USERCMS.USERUSDSC like '%' || :DESCRIPTION || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":DESCRIPTION", OracleDbType.Varchar2)).Value = user.Description;
                }

                if (!string.IsNullOrWhiteSpace(user.Status))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and USERCMS.USERSTAT = :Status ";
                    cmd.Parameters.Add(new OracleParameter(":Status", OracleDbType.Int32)).Value = user.Status;
                }

                if (user.StartDate != null)
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and  :StartDate >= USERCMS.USERSDAT ";
                    cmd.Parameters.Add(new OracleParameter(":StartDate", OracleDbType.Date)).Value = user.StartDate;
                }

                if (user.EndDate != null)
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and :EndDate <= USERCMS.USEREDAT ";
                    cmd.Parameters.Add(new OracleParameter(":EndDate", OracleDbType.Date)).Value = user.EndDate;
                }

                if (!string.IsNullOrWhiteSpace(user.UserType))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and USERCMS.USERTYPE = :Type ";
                    cmd.Parameters.Add(new OracleParameter(":Type", OracleDbType.Int32)).Value = user.UserType;
                }

                if (!string.IsNullOrWhiteSpace(user.IMEI))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and USERCMS.USERIMEI like '%' || :IMEI || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":IMEI", OracleDbType.Varchar2)).Value = user.IMEI;
                }

                if (!string.IsNullOrWhiteSpace(user.AccessProfile))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and USERCMS.USERACPROF like '%' || :AccessProfile || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":AccessProfile", OracleDbType.Varchar2)).Value = user.AccessProfile;
                }

                if (!string.IsNullOrWhiteSpace(user.MenuProfile))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and USERCMS.USERMEPROF like '%' || :MenuProfile || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":MenuProfile", OracleDbType.Varchar2)).Value = user.MenuProfile;
                }

                if (!string.IsNullOrWhiteSpace(user.SiteProfile))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and USERCMS.USERSTPROF like '%' || :SiteProfile || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":SiteProfile", OracleDbType.Varchar2)).Value = user.SiteProfile;
                }



                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllMenuFiltered(Menu menu)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select MENUMENUID as \"ID\", " +
                                  "MENUMENUNM as \"MENU NAME\", " +
                                  "MENUMEGRPID as \"MENU GROUP ID\", " +
                                  "MENUMEGRPNM as \"MENU GROUP NAME\", " +
                                  "MENUMEURL as \"MENU URL\" " +
                                  "from kdscmsmenu " +
                                  "where MENUMENUID is not null ";


                cmd.CommandType = CommandType.Text;

                if (!string.IsNullOrWhiteSpace(menu.MenuID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and MENUMENUID like '%' || :MenuID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":MenuID", OracleDbType.Varchar2)).Value = menu.MenuID;
                }

                if (!string.IsNullOrWhiteSpace(menu.MenuName))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and MENUMENUNM like '%' || :MenuName || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":MenuName", OracleDbType.Varchar2)).Value = menu.MenuName;
                }

                if (!string.IsNullOrWhiteSpace(menu.MenuGroupID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and MENUMEGRPID like '%' || :MenuGroupID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":MenuGroupID", OracleDbType.Varchar2)).Value = menu.MenuGroupID;
                }

                if (!string.IsNullOrWhiteSpace(menu.MenuGroupName))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and MENUMEGRPNM like '%' || :MenuGroupName || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":MenuGroupName", OracleDbType.Varchar2)).Value = menu.MenuGroupName;
                }

                if (!string.IsNullOrWhiteSpace(menu.MenuURL))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and MENUMEURL like '%' || :MenuURL || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":MenuURL", OracleDbType.Varchar2)).Value = menu.MenuURL;
                }

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllSiteFiltered(SiteMaster siteMaster)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "sitesite as \"SITE\", " +
                                  "sitesclas AS \"SITECLASS\", " +
                                  "sitesitename as \"Site NAME\", " +
                                  "SITESITEFLAG as \"Enable\" " +
                                  "from kdscmssite " +
                                  "WHERE sitesite IS NOT NULL ";


                cmd.CommandType = CommandType.Text;


                if (!string.IsNullOrWhiteSpace(siteMaster.Site))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and sitesite like '%' || :Site || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":Site", OracleDbType.Varchar2)).Value = siteMaster.Site;
                }

                if (siteMaster.SiteClass.HasValue)
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and sitesclas = :SiteClass ";
                    cmd.Parameters.Add(new OracleParameter(":SiteClass", OracleDbType.Int32)).Value =
                        siteMaster.SiteClass;
                }

                if (!string.IsNullOrWhiteSpace(siteMaster.SiteName))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and sitesitename like '%' || :SiteName || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":SiteName", OracleDbType.Varchar2)).Value = siteMaster.SiteName;
                }



                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllItemFiltered(ItemMaster item, String SiteClass)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "ITEMITEMIDX AS \"ITEM ID\", " +
                                  "(SELECT Par.Pardldesc from Kdscmspardtable par where Par.Pardsclas = :SiteClass and Par.Pardtabid = 13 and Par.Pardtabent = ITEMTYPE) AS \"TYPE\", " +
                                  "ITEMSDESC AS \"SHORT DESCRIPTION\", " +
                                  "ITEMLDESC AS \"LONG DESCRIPTION\", " +
                                  "BRNDDESC  AS \"BRAND\" from Kdscmsmstitem , KDSCMSMSTBRND " +
                                  "where ITEMITEMID IS NOT NULL and ITEMBRNDID = BRNDBRNDID ";

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new OracleParameter(":SiteClass", OracleDbType.Varchar2)).Value = SiteClass;

                if (!string.IsNullOrWhiteSpace(item.ItemIDExternal))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and ITEMITEMIDX like '%' || :ItemIDExt || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":ItemIDExt", OracleDbType.Varchar2)).Value = item.ItemIDExternal;
                }

                if (!string.IsNullOrWhiteSpace(item.Brand))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and ITEMBRNDID = :Brand ";
                    cmd.Parameters.Add(new OracleParameter(":Brand", OracleDbType.Varchar2)).Value = item.Brand;
                }

                if (!string.IsNullOrWhiteSpace(item.ShortDesc))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and ITEMSDESC like '%' || :SDesc || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":SDesc", OracleDbType.Varchar2)).Value = item.ShortDesc;
                }

                if (!string.IsNullOrWhiteSpace(item.LongDesc))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and ITEMLDESC like '%' || :LDesc || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":LDesc", OracleDbType.Varchar2)).Value = item.LongDesc;
                }

                if (!string.IsNullOrWhiteSpace(item.Type))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and ITEMTYPE = :Type ";
                    cmd.Parameters.Add(new OracleParameter(":Type", OracleDbType.Int32)).Value = item.Type;
                }



                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllTransactionTableFiltered(Stock StockDisplay, String SiteProfile)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                    "CMSTRNITEMID as \"ITEM ID\", " +
                                    "CMSTRNBRCD as BARCODE, " +
                                    "CMSTRNSITE as SITE, " +
                                    "'-' as DESCRIPTION, "+
                                    "SUM(CMSTRNQTY) as QUANTITY, " +
                                    "SUM(CMSTRNAMT) as AMOUNT " +
                                    "from KDSCMSTRN " +
                                    "where exists (select 1 from KDSCMSPROFSITELINK where PRSTSTPROF = :Siteprofile and PRSTSITE = CMSTRNSITE) ";


                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new OracleParameter(":Siteprofile", OracleDbType.Varchar2)).Value = SiteProfile;

                if (!string.IsNullOrWhiteSpace(StockDisplay.ItemID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and CMSTRNITEMID like '%' || :ItemID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":ItemID", OracleDbType.Varchar2)).Value = StockDisplay.ItemID;
                }

                if (!string.IsNullOrWhiteSpace(StockDisplay.Barcode))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and CMSTRNBRCD like '%' || :Barcode || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":Barcode", OracleDbType.Varchar2)).Value = StockDisplay.Barcode;
                }

                if (!string.IsNullOrWhiteSpace(StockDisplay.Site))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and CMSTRNSITE = :Site  ";
                    cmd.Parameters.Add(new OracleParameter(":Site", OracleDbType.Varchar2)).Value = StockDisplay.Site;
                }
                
                if (!string.IsNullOrWhiteSpace(StockDisplay.Desc))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and CMSTRNBRCD like '%' || :Barcode || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":Barcode", OracleDbType.Varchar2)).Value = StockDisplay.Barcode;
                }


                cmd.CommandText = cmd.CommandText +
                                      "GROUP BY CMSTRNSITE, CMSTRNITEMID, CMSTRNBRCD";

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        public DataTable GetAllTransactionTableHeaderMonitoring(Stock StockDisplay, String SiteProfile)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT SITE.SITESITE AS SITE , " +
                                  "(SELECT SITESITENAME FROM KDSCMSSITE WHERE SITESITE = SITE.SITESITE " +
                                  ")                               AS \"SITE NAME\", " +
                                  "COUNT(TRN.CMSTRNSITE)           AS TRANSACTION, " +
                                  "NVL(SUM(TRN.CMSTRNQTY), 0)      AS QUANTITY, " +
                                  "NVL(SUM(TRN.CMSNORMALPRICE), 0) AS \"NORMAL PRICE\", " +
                                  "NVL(SUM(TRN.CMSFINALPRICE), 0)  AS \"FINAL PRICE\", " +
                                  "CASE " +
                                  "  WHEN TRN.CMSTRSTAT = '1' " +
                                  "  THEN 'Sales' " +
                                  "  WHEN TRN.CMSTRSTAT = '2' " +
                                  "  THEN 'Return' " +
                                  "  WHEN TRN.CMSTRSTAT = '3' " +
                                  "  THEN 'Movement In' " +
                                  "  ELSE 'Unknown Type' " +
                                  "END AS TYPE, " +
                                  "(SELECT NVL(MAX(COUNT(TRN2.CMSTRNSITE)), 0) " +
                                  "FROM KDSCMSTRN TRN2 " +
                                  "WHERE TRN2.CMSTRNSITE = TRN.CMSTRNSITE " +
                                  "AND TRN2.CMSTRSTAT = TRN.CMSTRSTAT " +
                                  "AND TRN2.CMSTRNFLAG = 1 " +
                                  "AND to_date(TRN2.CMSTRNCDAT, 'DD-Mon-YY') >= to_date('" +
                                  StockDisplay.DateFrom.GetValueOrDefault().ToString("dd-MMM-yy") + "', 'DD-Mon-YY') " +
                                  "AND to_date(TRN2.CMSTRNCDAT, 'DD-Mon-YY') <= to_date('" +
                                  StockDisplay.DateEnd.GetValueOrDefault().ToString("dd-MMM-yy") + "', 'DD-Mon-YY') " +
                                  "AND EXISTS(select 1 from KDSCMSPROFSITELINK where PRSTSTPROF = :SiteProfile1 and PRSTSITE = TRN2.CMSTRNSITE) " +
                                  "AND TRN2.CMSTRNSITE = DECODE('" + StockDisplay.Site + "', '', TRN2.CMSTRNSITE, '" +
                                  StockDisplay.Site + "') " +
                                  "GROUP BY TRN2.CMSTRNSITE, " +
                                  "  TRN2.CMSTRSTAT " +
                                  ") " +
                                  "|| '/' " +
                                  "|| " +
                                  "(SELECT NVL(MAX(COUNT(TRN2.CMSTRNSITE)), 0) " +
                                  "FROM KDSCMSTRN TRN2 " +
                                  "WHERE TRN2.CMSTRNSITE = TRN.CMSTRNSITE " +
                                  "AND TRN2.CMSTRSTAT = TRN.CMSTRSTAT " +
                                  "AND to_date(TRN2.CMSTRNCDAT, 'DD-Mon-YY') >= to_date('" +
                                  StockDisplay.DateFrom.GetValueOrDefault().ToString("dd-MMM-yy") + "', 'DD-Mon-YY') " +
                                  "AND to_date(TRN2.CMSTRNCDAT, 'DD-Mon-YY') <= to_date('" +
                                  StockDisplay.DateEnd.GetValueOrDefault().ToString("dd-MMM-yy") + "', 'DD-Mon-YY') " +
                                  "AND EXISTS(select 1 from KDSCMSPROFSITELINK where PRSTSTPROF = :SiteProfile2 and PRSTSITE = TRN2.CMSTRNSITE) " +
                                  "AND TRN2.CMSTRNSITE = DECODE('" + StockDisplay.Site + "', '', TRN2.CMSTRNSITE, '" +
                                  StockDisplay.Site + "') " +
                                  "GROUP BY TRN2.CMSTRNSITE, " +
                                  "  TRN2.CMSTRSTAT " +
                                  ") AS PROGRESS " +
                                  "FROM KDSCMSSITE SITE " +
                                  "LEFT OUTER JOIN KDSCMSTRN TRN " +
                                  "ON SITE.SITESITE = TRN.CMSTRNSITE " +
                                  "AND SITE.SITESITE IS NOT NULL " +
                                  "AND to_date(TRN.CMSTRNCDAT, 'DD-Mon-YY') >= to_date('" +
                                  StockDisplay.DateFrom.GetValueOrDefault().ToString("dd-MMM-yy") + "', 'DD-Mon-YY') " +
                                  "AND to_date(TRN.CMSTRNCDAT, 'DD-Mon-YY') <= to_date('" +
                                  StockDisplay.DateEnd.GetValueOrDefault().ToString("dd-MMM-yy") + "', 'DD-Mon-YY') " +
                                  "WHERE SITE.SITESITE = DECODE('" + StockDisplay.Site + "', '', SITE.SITESITE, '" +
                                  StockDisplay.Site + "') " +
                                  "AND EXISTS(select 1 from KDSCMSPROFSITELINK where PRSTSTPROF = :SiteProfile3 and PRSTSITE = SITE.SITESITE) " +
                                  "GROUP BY SITE.SITESITE, " +
                                  "  TRN.CMSTRNSITE, " +
                                  "  TRN.CMSTRSTAT, " +
                                  "  TRN.CMSTRNFLAG ";


                cmd.Parameters.Add(new OracleParameter(":SiteProfile1", OracleDbType.Varchar2)).Value = SiteProfile;
                cmd.Parameters.Add(new OracleParameter(":SiteProfile2", OracleDbType.Varchar2)).Value = SiteProfile;
                cmd.Parameters.Add(new OracleParameter(":SiteProfile3", OracleDbType.Varchar2)).Value = SiteProfile;
                //cmd.Parameters.Add(new OracleParameter(":Sdate2", OracleDbType.Date)).Value = StockDisplay.DateFrom;
                //cmd.Parameters.Add(new OracleParameter(":Sdate3", OracleDbType.Date)).Value = StockDisplay.DateFrom;
                //cmd.Parameters.Add(new OracleParameter(":EDate1", OracleDbType.Date)).Value = StockDisplay.DateEnd;
                //cmd.Parameters.Add(new OracleParameter(":EDate2", OracleDbType.Date)).Value = StockDisplay.DateEnd;
                //cmd.Parameters.Add(new OracleParameter(":EDate3", OracleDbType.Date)).Value = StockDisplay.DateEnd;
                //cmd.Parameters.Add(new OracleParameter(":Site11", OracleDbType.Varchar2)).Value = StockDisplay.Site;
                //cmd.Parameters.Add(new OracleParameter(":Site12", OracleDbType.Varchar2)).Value = StockDisplay.Site;
                //cmd.Parameters.Add(new OracleParameter(":Site21", OracleDbType.Varchar2)).Value = StockDisplay.Site;
                //cmd.Parameters.Add(new OracleParameter(":Site22", OracleDbType.Varchar2)).Value = StockDisplay.Site;
                //cmd.Parameters.Add(new OracleParameter(":Site31", OracleDbType.Varchar2)).Value = StockDisplay.Site;
                //cmd.Parameters.Add(new OracleParameter(":Site32", OracleDbType.Varchar2)).Value = StockDisplay.Site;



                cmd.CommandType = CommandType.Text;
                
                OracleDataReader dr = cmd.ExecuteReader();

                
                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAllTransactionTableMovementFiltered Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        public DataTable GetAllTransactionTableMonitoring(Stock StockDisplay, String SiteProfile)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select TRN.CMSTRNSITE as SITE, " +
                                  "(SELECT SITESITENAME FROM KDSCMSSITE WHERE SITESITE = TRN.CMSTRNSITE " +
                                  ")                               AS \"SITE NAME\", " +
                                  //"TRN.CMSTRNOTA as NOTA, " +
                                  "TRN.CMSTRNCDAT as \"TRANSACTION DATE\", " +
                                  "TRN.CMSTRNBRCD as BARCODE,  " +
                                  "TRN.CMSTRNQTY as QUANTITY,  " +
                                  "TRN.CMSNORMALPRICE as \"NORMAL PRICE\", " +
                                  "TRN.CMSFINALPRICE as \"FINAL PRICE\", " +
                                  "TRN.CMSDISCOUNT as DISCOUNT, " +
                                  "CASE " +
                                  "WHEN TRN.CMSTRSTAT = '1' THEN 'Sales' " +
                                  "WHEN TRN.CMSTRSTAT = '2' THEN 'Return' " +
                                  "WHEN TRN.CMSTRSTAT = '3' THEN 'Movement In' " +
                                  "ELSE 'Unknown Type' END AS TYPE , " +
                                  "CASE " +
                                  "WHEN TRN.CMSTRNFLAG = '1' THEN 'Not Processed' " +
                                  "WHEN TRN.CMSTRNFLAG = '2' THEN 'Success' " +
                                  "WHEN TRN.CMSTRNFLAG = '3' THEN 'Error' " +
                                  "ELSE '' END AS STATUS , " +
                                  "CASE WHEN " + "TRN.CMSTRNTYPE = '1' THEN 'Browser' " +
                                  "ELSE 'Mobile' END AS \"Input By\" ," +
                                  "'' AS NOTE, " +
                                  "CMSTRNUSR as \"USER\" " +
                                  "from KDSCMSTRN TRN " +
                                  "where CMSTRNSITE = CMSTRNSITE and " +
                                  "TRN.CMSTRSTAT <> 9 ";
                cmd.CommandType = CommandType.Text;


                if ((StockDisplay.DateFrom.HasValue))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and to_date(TRN.CMSTRNCDAT, 'DD-Mon-YY') >= to_date(:Sdate , 'DD-Mon-YY')   ";
                    cmd.Parameters.Add(new OracleParameter(":Sdate", OracleDbType.Date)).Value = StockDisplay.DateFrom;
                }
                if (StockDisplay.DateEnd.HasValue)
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and to_date(TRN.CMSTRNCDAT, 'DD-Mon-YY') <= to_date(:EDate , 'DD-Mon-YY') ";
                    cmd.Parameters.Add(new OracleParameter(":EDate", OracleDbType.Date)).Value = StockDisplay.DateEnd;
                }

               
                if (!string.IsNullOrWhiteSpace(StockDisplay.Barcode))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRN.CMSTRNBRCD like '%' || :Barcode || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":Barcode", OracleDbType.Varchar2)).Value = StockDisplay.Barcode;
                }

                if (!string.IsNullOrWhiteSpace(StockDisplay.Site))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRN.CMSTRNSITE = :Site  ";
                    cmd.Parameters.Add(new OracleParameter(":Site", OracleDbType.Varchar2)).Value = StockDisplay.Site;
                }


                if (!string.IsNullOrWhiteSpace(StockDisplay.InputType))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRN.CMSTRNTYPE = '" + StockDisplay.InputType + "'  ";
                }

                if (!string.IsNullOrWhiteSpace(StockDisplay.TransactionType))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRN.CMSTRSTAT = :Stat  ";
                    cmd.Parameters.Add(new OracleParameter(":Stat", OracleDbType.Varchar2)).Value = StockDisplay.TransactionType;
                }

                

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAllTransactionTableMovementFiltered Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllTransactionTableMovementFiltered(Stock StockDisplay, String SiteProfile)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select TRN.CMSTRNID as \"TRANSACTION ID\", " +
                                    "TRN.CMSTRNOTA as NOTA , " +
                                    "TRN.CMSTRNCDAT as \"TRANSACTION DATE\", " + 
                                    "TRN.CMSTRNITEMID as \"ITEM ID\", "+
                                    "TRN.CMSTRNBRCD as BARCODE,  "+
                                    "TRN.CMSTRNSITE as SITE,  " +
                                    "TRN.CMSTRNQTY as QUANTITY,  " +
                                    "TRN.CMSTRNAMT as AMOUNT, "+
                                    "CASE WHEN TRN.CMSTRNTYPE = '1' THEN 'Sales Input' " +
                                    "WHEN TRN.CMSTRNTYPE = '2' THEN 'Sales Return' " +
                                    "WHEN TRN.CMSTRNTYPE = '3' THEN 'Movement In' " +
                                    "ELSE 'Unknown Type' END AS STATUS " +
                                    "from KDSCMSTRN TRN where CMSTRNSITE = CMSTRNSITE ";
                cmd.CommandType = CommandType.Text;

                if (!string.IsNullOrWhiteSpace(StockDisplay.ItemID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRN.CMSTRNITEMID like '%' || :ItemID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":ItemID", OracleDbType.Varchar2)).Value = StockDisplay.ItemID;
                }

                if (!string.IsNullOrWhiteSpace(StockDisplay.Barcode))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRN.CMSTRNBRCD like '%' || :Barcode || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":Barcode", OracleDbType.Varchar2)).Value = StockDisplay.Barcode;
                }

                if (!string.IsNullOrWhiteSpace(StockDisplay.Site))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRN.CMSTRNSITE = :Site  ";
                    cmd.Parameters.Add(new OracleParameter(":Site", OracleDbType.Varchar2)).Value = StockDisplay.Site;
                }

                if (!string.IsNullOrWhiteSpace(StockDisplay.TransactionID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRN.CMSTRNID like '%' || :TransID || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":TransID", OracleDbType.Varchar2)).Value = StockDisplay.TransactionID;
                }

                if (!string.IsNullOrWhiteSpace(StockDisplay.TransactionType))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRN.CMSTRNTYPE = '" + StockDisplay.TransactionType +"'  ";                    
                }

                if (!string.IsNullOrWhiteSpace(StockDisplay.Nota))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and TRN.CMSTRNOTA = :Nota  ";
                    cmd.Parameters.Add(new OracleParameter(":Nota", OracleDbType.Varchar2)).Value = StockDisplay.Nota;
                }


               

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAllTransactionTableMovementFiltered Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetTransactionFilter(ItemMaster item, String SiteClass)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "ITEMITEMIDX AS \"ITEM ID\", " +
                                  "(SELECT Par.Pardldesc from Kdscmspardtable par where Par.Pardsclas = :SiteClass and Par.Pardtabid = 13 and Par.Pardtabent = ITEMTYPE) AS \"TYPE\", " +
                                  "ITEMSDESC AS \"SHORT DESCRIPTION\", " +
                                  "ITEMLDESC AS \"LONG DESCRIPTION\", " +
                                  "ITEMBRNDID AS \"BRAND\" from Kdscmsmstitem " +
                                  "where ITEMITEMID IS NOT NULL ";

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new OracleParameter(":SiteClass", OracleDbType.Varchar2)).Value = SiteClass;

                if (!string.IsNullOrWhiteSpace(item.ItemIDExternal))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and ITEMITEMIDX like '%' || :ItemIDExt || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":ItemIDExt", OracleDbType.Varchar2)).Value = item.ItemIDExternal;
                }

                if (!string.IsNullOrWhiteSpace(item.Brand))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and ITEMBRNDID = :Brand ";
                    cmd.Parameters.Add(new OracleParameter(":Brand", OracleDbType.Varchar2)).Value = item.Brand;
                }

                if (!string.IsNullOrWhiteSpace(item.ShortDesc))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and ITEMSDESC like '%' || :SDesc || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":SDesc", OracleDbType.Varchar2)).Value = item.ShortDesc;
                }

                if (!string.IsNullOrWhiteSpace(item.LongDesc))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and ITEMLDESC like '%' || :LDesc || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":LDesc", OracleDbType.Varchar2)).Value = item.LongDesc;
                }

                if (!string.IsNullOrWhiteSpace(item.Type))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and ITEMTYPE = :Type ";
                    cmd.Parameters.Add(new OracleParameter(":Type", OracleDbType.Int32)).Value = item.Type;
                }



                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetItemVarianFiltered(ItemVariant itemVar)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "ITEM.ITEMITEMIDX as \"ITEM ID\", " +
                                  "Variant.Vrntvrntidx as \"VARIANT ID\", " +
                                  "Variant.Vrntsdesc as \"SHORT DESCRIPTION\", " +
                                  "Variant.Vrntldesc as \"LONG DESCRIPTION\" " +
                                  "from kdscmsmstitem ITEM INNER JOIN KDSCMSMSTVRNT VARIANT ON Item.Itemitemid = Variant.VRNTITEMID " +
                                  "WHERE ITEM.ITEMITEMID IS NOT NULL ";


                cmd.CommandType = CommandType.Text;


                if (!string.IsNullOrWhiteSpace(itemVar.ItemIDExternal))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and ITEM.ITEMITEMIDX like '%' || :ItemIDExt || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":ItemIDExt", OracleDbType.Varchar2)).Value = itemVar.ItemIDExternal;
                }

                if (!string.IsNullOrWhiteSpace(itemVar.VariantIDExternal))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and Variant.VrntvrntidX like '%' || :VariantIDExt || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":VariantIDExt", OracleDbType.Varchar2)).Value = itemVar.VariantIDExternal;
                }

                if (!string.IsNullOrWhiteSpace(itemVar.ShortDesc))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and Variant.Vrntsdesc like '%' || :VarSDesc || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":VarSDesc", OracleDbType.Varchar2)).Value = itemVar.ShortDesc;
                }

                if (!string.IsNullOrWhiteSpace(itemVar.LongDesc))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and Variant.Vrntldesc like '%' || :VarLDesc || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":VarLDesc", OracleDbType.Varchar2)).Value = itemVar.LongDesc;
                }


                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllVariantFiltered(VariantMaster variant, String SiteClass)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "VRNTVRNTIDX as \"VARIANT ID\", " +
                                  "VRNTSDESC AS \"SHORT DESCRIPTION\", " +
                                  "VRNTLDESC AS \"LONG DESCRIPTION\", " +
                                  "(SELECT Par.Pardldesc from Kdscmspardtable par where Par.Pardsclas = :SiteClass and Par.Pardtabid = 5 and Par.Pardtabent = VRNTSTAT) as \"STATUS\" " +
                                  "from KDSCMSMSTVRNT " +
                                  "where VRNTITEMID  = :ItemID ";


                cmd.Parameters.Add(new OracleParameter(":SiteClass", OracleDbType.Int32)).Value = SiteClass;
                cmd.Parameters.Add(new OracleParameter(":ItemID", OracleDbType.Int32)).Value = variant.ItemID;
                cmd.CommandType = CommandType.Text;


                if (!string.IsNullOrWhiteSpace(variant.VariantIDExternal))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and VRNTVRNTIDX like '%' || :VariantIDExternal || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":VariantIDExternal", OracleDbType.Varchar2)).Value = variant.VariantIDExternal;
                }

                if (!string.IsNullOrWhiteSpace(variant.ShortDesc))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and VRNTSDESC like '%' || :SDesc || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":SDesc", OracleDbType.Varchar2)).Value = variant.ShortDesc;
                }

                if (!string.IsNullOrWhiteSpace(variant.LongDesc))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and VRNTLDESC like '%' || :LDesc || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":LDesc", OracleDbType.Varchar2)).Value = variant.LongDesc;
                }

                if (!string.IsNullOrWhiteSpace(variant.Status))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and VRNTSTAT = :Status ";
                    cmd.Parameters.Add(new OracleParameter(":Status", OracleDbType.Int32)).Value = variant.Status;
                }



                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }



        public DataTable GetAllBarcodeFiltered(BarcodeMaster barcode, String SiteClass)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select BARCODE.Brcdbrcdid as BARCODE, " +
                                  "BARCODE.BRCDSDAT as \"START DATE\", " +
                                  "BARCODE.BRCDEDAT as \"END DATE\", " +
                                  "(SELECT Par.Pardldesc from Kdscmspardtable par where Par.Pardsclas = :SiteClass and Par.Pardtabid = 16 and Par.Pardtabent = Barcode.Brcdtype) as \"BARCODE TYPE\", " +
                                  "(SELECT Par.Pardldesc from Kdscmspardtable par where Par.Pardsclas = :SiteClass2 and Par.Pardtabid = 6 and Par.Pardtabent = Barcode.Brcdstat) as \"BARCODE STATUS\" " +
                                  "from KDSCMSMSTBRCD BARCODE " +
                                  "where BARCODE.Brcditemid = :ItemID " +
                                  "and BARCODE.Brcdvrntid = :VariantID ";


                cmd.Parameters.Add(new OracleParameter(":SiteClass", OracleDbType.Int32)).Value = SiteClass;
                cmd.Parameters.Add(new OracleParameter(":SiteClass2", OracleDbType.Int32)).Value = SiteClass;
                cmd.Parameters.Add(new OracleParameter(":ItemID", OracleDbType.Int32)).Value = barcode.ItemID;
                cmd.Parameters.Add(new OracleParameter(":VariantID", OracleDbType.Int32)).Value = barcode.VariantID;
                cmd.CommandType = CommandType.Text;

                if (!string.IsNullOrWhiteSpace(barcode.Barcode))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and Barcode.Brcdbrcdid like '%' || :Barcode || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":Barcode", OracleDbType.Varchar2)).Value = barcode.Barcode;
                }

                if (!string.IsNullOrWhiteSpace(barcode.Status))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and Barcode.Brcdstat like '%' || :Status || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":Status", OracleDbType.Varchar2)).Value = barcode.Status;
                }

                if (!string.IsNullOrWhiteSpace(barcode.Type))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and Barcode.Brcdtype like '%' || :Type || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":Type", OracleDbType.Varchar2)).Value = barcode.Type;
                }

                if (barcode.StartDate.HasValue)
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and  :StartDate >= Barcode.BRCDSDAT ";
                    cmd.Parameters.Add(new OracleParameter(":StartDate", OracleDbType.Date)).Value = barcode.StartDate;
                }

                if (barcode.StartDate.HasValue)
                {
                    cmd.CommandText = cmd.CommandText +
                                     "and  :EndDate >= Barcode.BRCDEDAT ";
                    cmd.Parameters.Add(new OracleParameter(":EndDate", OracleDbType.Date)).Value = barcode.EndDate;
                }


                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllSiteProfileFiltered(SiteProfileHeader siteProfile)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "Stprstprof as \"SITE PROFILE\", " +
                                  "Stprstdesc as \"PROFILE DESCRIPTION\" " +
                                  "from Kdscmssiteprof " +
                                  "WHERE Stprstprof IS NOT NULL ";


                cmd.CommandType = CommandType.Text;

                if (!string.IsNullOrWhiteSpace(siteProfile.Profile))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and Stprstprof like '%' || :SiteProfile || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":SiteProfile", OracleDbType.Varchar2)).Value = siteProfile.Profile;
                }

                if (!string.IsNullOrWhiteSpace(siteProfile.Description))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and Stprstdesc like '%' || :Description || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":Description", OracleDbType.Varchar2)).Value = siteProfile.Description;
                }




                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllMenuProfileFiltered(MenuProfileHeader menuProfile)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "Meprhmeprof AS  \"MENU PROFILE\", " +
                                  "Meprhmedesc AS \"DESCRIPTION\" " +
                                  "from kdscmsmeprofh " +
                                  "where Meprhmeprof is not null ";


                cmd.CommandType = CommandType.Text;

                if (!string.IsNullOrWhiteSpace(menuProfile.Profile))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and Meprhmeprof like '%' || :MenuProfile || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":MenuProfile", OracleDbType.Varchar2)).Value = menuProfile.Profile;
                }

                if (!string.IsNullOrWhiteSpace(menuProfile.Description))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and Meprhmedesc like '%' || :Description || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":Description", OracleDbType.Varchar2)).Value = menuProfile.Description;
                }




                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllAccessProfileFiltered(AccessProfileHeader accessProfile)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "ACPRHACPROF as \"ACCESS PROFILE\", " +
                                  "ACPRHACDESC as \"DESCRIPTION\"  " +
                                  "from Kdscmsaccprofh " +
                                  "where ACPRHACPROF is not null ";


                cmd.CommandType = CommandType.Text;

                if (!string.IsNullOrWhiteSpace(accessProfile.Profile))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and ACPRHACPROF like '%' || :AccessProfile || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":AccessProfile", OracleDbType.Varchar2)).Value = accessProfile.Profile;
                }

                if (!string.IsNullOrWhiteSpace(accessProfile.Description))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and ACPRHACDESC like '%' || :Description || '%' ";
                    cmd.Parameters.Add(new OracleParameter(":Description", OracleDbType.Varchar2)).Value = accessProfile.Description;
                }

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllSPGSPV()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT SPGSPV.USERID, SPGSPV.USERNAME, SPGSPV.PASSWORD, SPGSPV.STORE, SPGSPV.SUPER AS SUPERIOR, " +
                                  "DECODE(SPGSPV.POSITION, 0, 'SPG', 1, 'Supervisor', 2, 'Manager', 'Unknown Code') POSITION, " +
                                  "SPGSPV.STARTDATE AS \"START DATE\", " +
                                  "SPGSPV.ENDDATE AS \"END DATE\" " +
                                  "FROM KDSSPGSPVCMSV3 SPGSPV";
                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetItemMaster()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT ITEM.ID, " +
                                  "ITEM.ITEMID, " +
                                  "ITEM.BRAND, " +
                                  "ITEM.ITEMNAME AS \"NAME\", " +
                                  "ITEM.BARCODE, " +
                                  "ITEM.ITEMSIZE AS \"SIZE\",  " +
                                  "ITEM.VARIANT, " +
                                  "CASE WHEN ITEM.STATUS = '0' THEN 'Disable' ELSE 'Enable' END AS STATUS FROM KDSMASTERITEMCMSV3 ITEM";
                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();


                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetItemMaster Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllProfile()
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT PROFILEID, PROFILENAME FROM KDSPROFILECMSV3 ORDER BY PROFILENAME";
                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllBrandByProfileID(String ProfileID)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT BRANDCODE, BRANDNAME FROM KDSBRANDCMSV3 " +
                                  "ORDER BY BRANDCODE";
                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAllBrandByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetAllVariant(String ProfileID)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT VARIANTCODE, VARIANTNAME FROM KDSVARIANTCMSV3 " +
                                  "ORDER BY VARIANTNAME";
                cmd.CommandType = CommandType.Text;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetAllVariant Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }


        public DataTable GetSiteDataExcludeByProfileID(String ProfileID)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT SITECODE, SITENAME FROM KDSSITECMSV3 WHERE SITECODE NOT IN(SELECT SITEID FROM KDSPROFILESITELINKCMSV3 WHERE PROFILEID = :ProfileId ) ORDER BY SITENAME";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":ProfileId", OracleDbType.Varchar2)).Value = ProfileID;


                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataExcludeByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public string insertMenuByProfileID(String MenuID, String ProfileID, String UserID)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "INSERT INTO KDSPROFILEMENULINKCMSV3 (MENUID, PROFILEID, CREATEDBY, CREATEDDATE) VALUES (:MenuId, :ProfileId, :UserId, SYSDATE)";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":MenuId", OracleDbType.Varchar2)).Value = MenuID;
                cmd.Parameters.Add(new OracleParameter(":ProfileId", OracleDbType.Varchar2)).Value = ProfileID;
                cmd.Parameters.Add(new OracleParameter(":UserId", OracleDbType.Varchar2)).Value = UserID;

                logger.Debug(cmd.CommandText);

                cmd.ExecuteNonQuery();

                this.Close();
                ResultString = "Success";
                return ResultString;
            }
            catch (Exception e)
            {
                logger.Error("insertMenuByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
       
        public OutputMessage InsertParameterHeader(ParameterHeader parHeader, String User)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSPARHTABLE.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PPARHTABID", OracleDbType.Int32).Value = parHeader.ID;
                cmd.Parameters.Add("PPARHTABNM", OracleDbType.Varchar2, 50).Value = parHeader.Name;
                cmd.Parameters.Add("PPARHSCLAS", OracleDbType.Int32).Value = parHeader.SClass;
                cmd.Parameters.Add("PPARHCOPY", OracleDbType.Int32).Value = parHeader.Copy;
                cmd.Parameters.Add("PPARHTABCOM", OracleDbType.Varchar2, 50).Value = parHeader.Comment;
                cmd.Parameters.Add("PPARHTABLOCK", OracleDbType.Int32).Value = parHeader.Lock;
                cmd.Parameters.Add("PPARHINTF", OracleDbType.Varchar2, 50).Value = "0";
                cmd.Parameters.Add("PPARHCRBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        //Sizegroup

        public OutputMessage InsertSizeGroup(SizeGroup sizegroup, String User)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSIZEGRP.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add("PSZGRSZGID", OracleDbType.Varchar2, 50).Value = sizegroup.SIZEID;
                cmd.Parameters.Add("PSZGRDESC", OracleDbType.Varchar2, 50).Value = sizegroup.SIZEDESC;
                cmd.Parameters.Add("PSZGRINTF", OracleDbType.Varchar2, 50).Value = 1;
                cmd.Parameters.Add("PSZGRCRBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }


        public OutputMessage InsertSizeDetail(SizeGroupDetail sizegroupdetail, String User)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSIZETBL.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add("PSIZESZGID", OracleDbType.Varchar2, 50).Value = sizegroupdetail.GID;
                cmd.Parameters.Add("PSIZESZID", OracleDbType.Varchar2, 50).Value = sizegroupdetail.ID;
                cmd.Parameters.Add("PSIZESDES", OracleDbType.Varchar2, 50).Value = sizegroupdetail.SizeSDesc;
                cmd.Parameters.Add("PSIZELDES", OracleDbType.Varchar2, 50).Value = sizegroupdetail.SizeLDesc;
                cmd.Parameters.Add("PSIZEORDR", OracleDbType.Int32, 50).Value = sizegroupdetail.SizeOrder;
                cmd.Parameters.Add("PSIZEINTF", OracleDbType.Int32, 50).Value = 1;
                cmd.Parameters.Add("PSIZECRBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage UpdateSizeDetail(SizeGroupDetail sizegroupdetail, String User)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSIZETBL.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PSIZESZGID", OracleDbType.Varchar2, 50).Value = sizegroupdetail.GID;
                cmd.Parameters.Add("PSIZESZID", OracleDbType.Varchar2, 50).Value = sizegroupdetail.ID;
                cmd.Parameters.Add("PSIZESDES", OracleDbType.Varchar2, 50).Value = sizegroupdetail.SizeSDesc;
                cmd.Parameters.Add("PSIZELDES", OracleDbType.Varchar2, 50).Value = sizegroupdetail.SizeLDesc;
                cmd.Parameters.Add("PSIZEORDR", OracleDbType.Int32, 50).Value = sizegroupdetail.SizeOrder;
                cmd.Parameters.Add("PSIZEINTF", OracleDbType.Int32, 50).Value = 1;
                cmd.Parameters.Add("PSIZEMOBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }





        public OutputMessage InsertColorGroup(ColorGroup colorgroup, String User)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSCOLGRP.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("PCOGRCOGID", OracleDbType.Varchar2, 50).Value = colorgroup.ID;
                cmd.Parameters.Add("PCOGRDESC", OracleDbType.Varchar2, 50).Value = colorgroup.Color;
                cmd.Parameters.Add("PCOGRCRBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("PCOGRINTF", OracleDbType.Int32, 50).Value = 1;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage InsertColorDetail(ColorGroupDetail colorgroupdetail, String User)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSCOLTBL.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("COLRCOGID", OracleDbType.Varchar2, 50).Value = colorgroupdetail.GID;
                cmd.Parameters.Add("COLRCOLID", OracleDbType.Varchar2, 50).Value = colorgroupdetail.ID;
                cmd.Parameters.Add("COLRSDES", OracleDbType.Varchar2, 50).Value = colorgroupdetail.ColorSDesc;
                cmd.Parameters.Add("COLRLDES", OracleDbType.Varchar2, 50).Value = colorgroupdetail.ColorLDesc;
                cmd.Parameters.Add("COLRORDR", OracleDbType.Int32, 50).Value = colorgroupdetail.ColorOrder;
                cmd.Parameters.Add("COLRINTF", OracleDbType.Varchar2, 50).Value = 1;
                cmd.Parameters.Add("COLRCRBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage UpdateColorDetail(ColorGroupDetail colorgroupdetail, String User)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSCOLTBL.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;




                cmd.Parameters.Add("COLRCOGID", OracleDbType.Varchar2, 50).Value = colorgroupdetail.GID;
                cmd.Parameters.Add("COLRCOLID", OracleDbType.Varchar2, 50).Value = colorgroupdetail.ID;
                cmd.Parameters.Add("COLRSDES", OracleDbType.Varchar2, 50).Value = colorgroupdetail.ColorSDesc;
                cmd.Parameters.Add("COLRLDES", OracleDbType.Varchar2, 50).Value = colorgroupdetail.ColorLDesc;
                cmd.Parameters.Add("COLRORDR", OracleDbType.Int32, 50).Value = colorgroupdetail.ColorOrder;
                cmd.Parameters.Add("COLRINTF", OracleDbType.Varchar2, 50).Value = 1;
                cmd.Parameters.Add("PCOLRMOBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage InsertParameterDetail(ParameterDetail parDetail, String User, String Copy)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSPARDTABLE.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PPARDTABID", OracleDbType.Int32).Value = parDetail.ID;
                cmd.Parameters.Add("PPARDTABENT", OracleDbType.Int32).Value = parDetail.Entry;
                cmd.Parameters.Add("PPARDSDESC", OracleDbType.Varchar2, 50).Value = parDetail.ShortDescription;
                cmd.Parameters.Add("PPARDLDESC", OracleDbType.Varchar2, 100).Value = parDetail.LongDescription;
                cmd.Parameters.Add("PPARDSCLAS", OracleDbType.Int32).Value = parDetail.SiteClass;
                cmd.Parameters.Add("PPARDVAN1", OracleDbType.Int32).Value = parDetail.Number1.HasValue
                   ? parDetail.Number1
                   : (object)DBNull.Value;

                cmd.Parameters.Add("PPARDVAN2", OracleDbType.Int32).Value = parDetail.Number2.HasValue
                   ? parDetail.Number2
                   : (object)DBNull.Value;

                cmd.Parameters.Add("PPARDVAN3", OracleDbType.Int32).Value = parDetail.Number3.HasValue
                   ? parDetail.Number3
                   : (object)DBNull.Value;

                cmd.Parameters.Add("PPARDVAN4", OracleDbType.Int32).Value = parDetail.Number4.HasValue
                   ? parDetail.Number4
                   : (object)DBNull.Value;


                cmd.Parameters.Add("PPARDVAC1", OracleDbType.Varchar2, 50).Value = parDetail.Char1;
                cmd.Parameters.Add("PPARDVAC2", OracleDbType.Varchar2, 50).Value = parDetail.Char2;
                cmd.Parameters.Add("PPARDVAC3", OracleDbType.Varchar2, 50).Value = parDetail.Char3;

                cmd.Parameters.Add("PPARDDATE1", OracleDbType.Date).Value = parDetail.Date1.HasValue
                   ? parDetail.Date1
                   : (object)DBNull.Value;

                cmd.Parameters.Add("PPARDDATE2", OracleDbType.Date).Value = parDetail.Date2.HasValue
                   ? parDetail.Date2
                   : (object)DBNull.Value;

                cmd.Parameters.Add("PPARDDATE3", OracleDbType.Date).Value = parDetail.Date3.HasValue
                   ? parDetail.Date3
                   : (object)DBNull.Value;
                cmd.Parameters.Add("PPARDCOMM", OracleDbType.Varchar2, 1000).Value = parDetail.Comment;
                cmd.Parameters.Add("PPARDINTF", OracleDbType.Varchar2, 50).Value = "0";
                cmd.Parameters.Add("PPARDCRBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("PCOPY", OracleDbType.Int32).Value = Copy;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;

                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public OutputMessage DeleteSizeHeader(String GrpID)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSIZEGRP.DEL_DATA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("PSZGRSZGID", OracleDbType.Varchar2, 50).Value = GrpID;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public OutputMessage DeleteParameterHeader(String ParamHeaderID, String SiteClass, String Copy)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSPARHTABLE.DEL_DATA";
                cmd.CommandType = CommandType.StoredProcedure;




                cmd.Parameters.Add("PPARHTABID", OracleDbType.Int32).Value = ParamHeaderID;
                cmd.Parameters.Add("PPARHSCLAS", OracleDbType.Int32).Value = SiteClass;
                cmd.Parameters.Add("PCOPY", OracleDbType.Int32).Value = Copy;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public OutputMessage DeleteSalesDetail(String TID, String IID, String NOTA, String Receiptid, String Site, String LINE)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSLSD.DEL_DATABYROWID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("PSLSDSLID", OracleDbType.Varchar2, 50).Value = TID;
                cmd.Parameters.Add("PSLSDSLIDI", OracleDbType.Int32).Value = IID;
                cmd.Parameters.Add("PSLSDSLNOTA", OracleDbType.Varchar2, 50).Value = NOTA;
                cmd.Parameters.Add("PSLSDRCPTID", OracleDbType.Varchar2, 50).Value = Receiptid;
                cmd.Parameters.Add("PSLSDLNNUM", OracleDbType.Varchar2, 50).Value = LINE;
                cmd.Parameters.Add("PSLSDSITE", OracleDbType.Varchar2, 50).Value = Site;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public OutputMessage DeleteColorDetail(String ColorID, String ColorGrpID)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSCOLTBL.DEL_DATA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("PCOLRCOGID", OracleDbType.Varchar2, 50).Value = ColorGrpID;
                cmd.Parameters.Add("PCOLRCOLID", OracleDbType.Varchar2, 50).Value = ColorID;

                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public OutputMessage DeleteColorHeader(String ColorGrpID)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSCOLGRP.DEL_DATABYCOGRCOGID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("PCOGRCOGID", OracleDbType.Varchar2, 50).Value = ColorGrpID;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public OutputMessage DeleteBrandHeader(String PBRNDBRNDID)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSMSTBRND.DEL_DATABYBRNDBRNDID";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PBRNDBRNDID", OracleDbType.Varchar2, 50).Value = PBRNDBRNDID;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public OutputMessage DeleteSiteMaster(String Site)
        {

            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSITE.DEL_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PSITESITE", OracleDbType.Varchar2, 20).Value = Site;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage DeleteUser(String UserID, String CurrUser)
        {

            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSUSER.DEL_DATABYUSERUSID";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PUSERUSID", OracleDbType.Varchar2, 20).Value = UserID;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage DeleteMenu(String MenuID, String CurrUser)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSMENU.DEL_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PMENUMENUID", OracleDbType.Varchar2, 20).Value = MenuID;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public String getinternalitemid(string itemidx)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select ITEMITEMID from KDSCMSMSTITEM where ITEMITEMIDX = '" + itemidx + "'";


                cmd.CommandType = CommandType.Text;
                
                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                String result = "";

                while (dr.Read())
                {
                    result = dr["ITEMITEMID"].ToString(); ;
                }

                this.Close();
                return result;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }
        
        public String GetSiteProfileHeaderRowID(SiteProfileHeader siteProfileHeader)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select rowid " +
                                  "from kdscmssiteprof " +
                                  "where Stprstprof = :SiteProfile " +
                                  "and Stprstdesc = :SiteProfileDesc;";


                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":SiteProfile", OracleDbType.Int32)).Value = siteProfileHeader.Profile;
                cmd.Parameters.Add(new OracleParameter(":SiteProfileDesc", OracleDbType.Int32)).Value = siteProfileHeader.Description;

                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                String Rowid = "";

                while (dr.Read())
                {
                    Rowid = dr["rowid"].ToString(); ;
                }

                this.Close();
                return Rowid;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public OutputMessage DeleteSiteProfileHeader(SiteProfileHeader siteProfile)
        {

            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSITEPROF.DEL_DATA";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add("PSTPRSTPROF", OracleDbType.Varchar2, 20).Value = siteProfile.Profile;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage DeleteMenuProfileHeader(MenuProfileHeader menuProfile)
        {

            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSMEPROFH.DEL_DATABYPROFID";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add("PMEPRHMEPROF", OracleDbType.Varchar2, 20).Value = menuProfile.Profile;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }


        public OutputMessage DeleteAccessProfileHeader(AccessProfileHeader accessProfile)
        {

            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSACCPROFH.DEL_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PACPRHACPROF", OracleDbType.Varchar2, 20).Value = accessProfile.Profile;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage DeleteSiteProfileLink(SiteProfileLink siteProfileLink)
        {
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();


                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSPROFSITELINK.DEL_DATA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("PPRSTSTPROF", OracleDbType.Varchar2, 50).Value = siteProfileLink.SiteProfile;
                cmd.Parameters.Add("PPRSTSITE", OracleDbType.Varchar2, 50).Value = siteProfileLink.Site;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;

                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");

                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;


            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage DeleteMenuProfileLink(MenuProfileLink menuProfileLink)
        {
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();


                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSMEPROFD.DEL_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PMEPRDMEPROF", OracleDbType.Varchar2, 50).Value = menuProfileLink.MenuProfile;
                cmd.Parameters.Add("PMEPRDMENUID", OracleDbType.Varchar2, 50).Value = menuProfileLink.MenuID;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;

                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");

                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;


            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }


        public OutputMessage DeleteParameterDetail(String ParamDetailID, String SiteClass, String Entry, String Copy)
        {
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSPARDTABLE.DEL_DATA";
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add("PPARDTABID", OracleDbType.Int32).Value = ParamDetailID;
                cmd.Parameters.Add("PPARDTABENT", OracleDbType.Int32).Value = Entry;
                cmd.Parameters.Add("PPARDSCLAS", OracleDbType.Int32).Value = SiteClass;
                cmd.Parameters.Add("PCOPY", OracleDbType.Int32).Value = Copy;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage DeleteVariantMaster(String VariantIDExternal, String ItemID)
        {
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSMSTVRNT.DEL_DATA";
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add("PVRNTITEMID", OracleDbType.Int32).Value = ItemID;
                cmd.Parameters.Add("PVRNTVRNTIDX", OracleDbType.Varchar2, 20).Value = VariantIDExternal;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage DeleteBarcode(BarcodeMaster barcode)
        {
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSMSTBRCD.DEL_DATA";
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add("PBRCDBRCDID,", OracleDbType.Varchar2, 20).Value = barcode.Barcode;
                cmd.Parameters.Add("PBRCDITEMID,", OracleDbType.Int32).Value = barcode.ItemID;
                cmd.Parameters.Add("PBRCDVRNTID,", OracleDbType.Int32).Value = barcode.VariantID;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage DeleteVariantDetail(String VariantID)
        {
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSDTLVRNT.DEL_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PDTLVRNTVRNTID", OracleDbType.Int32).Value = VariantID;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage DeleteItem(String ItemIDExternal)
        {
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSMSTITEM.DEL_DATA";
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add("PITEMITEMIDX", OracleDbType.Int32).Value = ItemIDExternal;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;



                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage updateSizeHeader(SizeGroup sizegroup, String User)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSIZEGRP.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PSZGRSZGID", OracleDbType.Varchar2, 50).Value = sizegroup.SIZEID;
                cmd.Parameters.Add("PSZGRDESC", OracleDbType.Varchar2, 50).Value = sizegroup.SIZEDESC;
                cmd.Parameters.Add("PSZGRINTF", OracleDbType.Varchar2).Value = 0;
                cmd.Parameters.Add("PSZGRMOBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage updateColorHeader(ColorGroup colorgroup, String User)
        {

            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSCOLGRP.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PCOGRCOGID", OracleDbType.Varchar2, 50).Value = colorgroup.ID;
                cmd.Parameters.Add("PCOGRDESC", OracleDbType.Varchar2, 50).Value = colorgroup.Color;
                cmd.Parameters.Add("PCOGRMOBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("PCOGRINTF", OracleDbType.Int32).Value = 0;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }




        public OutputMessage updateParameterHeader(ParameterHeader parHeader, String User)
        {
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSPARHTABLE.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("PPARHTABID", OracleDbType.Int32).Value = parHeader.ID;
                cmd.Parameters.Add("PPARHTABNM", OracleDbType.Varchar2, 50).Value = parHeader.Name;
                cmd.Parameters.Add("PPARHSCLAS", OracleDbType.Int32).Value = string.IsNullOrWhiteSpace(parHeader.SClass)
                   ? (object)DBNull.Value
                   : parHeader.SClass;
                cmd.Parameters.Add("PPARHCOPY", OracleDbType.Int32).Value = string.IsNullOrWhiteSpace(parHeader.Copy)
                   ? (object)DBNull.Value
                   : parHeader.Copy;
                cmd.Parameters.Add("PPARHTABCOM", OracleDbType.Varchar2, 1000).Value = parHeader.Comment;
                cmd.Parameters.Add("PPARHTABLOCK", OracleDbType.Int32).Value = string.IsNullOrWhiteSpace(parHeader.Lock)
                   ? (object)DBNull.Value
                   : parHeader.Lock;
                cmd.Parameters.Add("PPARHINTF", OracleDbType.Varchar2, 1).Value = "0";
                cmd.Parameters.Add("PPARHMOBY", OracleDbType.Varchar2, 20).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }



        public OutputMessage updateItemMaster(ItemMaster item, String User)
        {
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSMSTITEM.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("PITEMITEMIDX", OracleDbType.Varchar2, 30).Value = item.ItemIDExternal;
                cmd.Parameters.Add("PITEMTYPE", OracleDbType.Int32).Value = item.Type;
                cmd.Parameters.Add("PITEMSDESC", OracleDbType.Varchar2, 30).Value = item.ShortDesc;
                cmd.Parameters.Add("PITEMLDESC", OracleDbType.Varchar2, 50).Value = item.LongDesc;
                cmd.Parameters.Add("PITEMBRNDID", OracleDbType.Varchar2, 3).Value = item.Brand;
                cmd.Parameters.Add("PITEMMOBY", OracleDbType.Varchar2, 20).Value = User;
                cmd.Parameters.Add("PITEMINTF", OracleDbType.Varchar2, 1).Value = "0";
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage updateVariantMaster(VariantMaster variant, String User)
        {
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSMSTVRNT.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PVRNTITEMID", OracleDbType.Int32).Value = variant.ItemID;
                cmd.Parameters.Add("PVRNTVRNTIDX", OracleDbType.Varchar2, 20).Value = variant.VariantIDExternal;
                cmd.Parameters.Add("PVRNTSDESC", OracleDbType.Varchar2, 20).Value = variant.ShortDesc;
                cmd.Parameters.Add("PVRNTLDESC", OracleDbType.Varchar2, 50).Value = variant.LongDesc;
                cmd.Parameters.Add("PVRNTSTAT", OracleDbType.Int32).Value = variant.Status;
                cmd.Parameters.Add("PVRNTMOBY", OracleDbType.Varchar2, 20).Value = User;
                cmd.Parameters.Add("PVRNTDINTF", OracleDbType.Varchar2, 1).Value = "0";
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage updateVariantDetail(VariantDetail variantDetail, String User)
        {
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSDTLVRNT.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PDTLVRNTVRNTID", OracleDbType.Int32).Value = variantDetail.VariantID;
                cmd.Parameters.Add("PDTLVRNTSZGID", OracleDbType.Varchar2, 20).Value = variantDetail.SizeGroup;
                cmd.Parameters.Add("PDTLVRNTSZID", OracleDbType.Varchar2, 20).Value = variantDetail.SizeDetail;
                cmd.Parameters.Add("PDTLVRNTCOGID", OracleDbType.Varchar2, 20).Value = variantDetail.ColorGroup;
                cmd.Parameters.Add("PDTLVRNTCOLID", OracleDbType.Varchar2, 20).Value = variantDetail.ColorDetail;
                cmd.Parameters.Add("PDTLVRNTSTGID", OracleDbType.Varchar2, 20).Value = variantDetail.StyleGroup;
                cmd.Parameters.Add("PDTLVRNTSTYLID", OracleDbType.Varchar2, 20).Value = variantDetail.StyleDetail;
                cmd.Parameters.Add("PDTLVRNTDINTF", OracleDbType.Varchar2, 1).Value = "0";
                cmd.Parameters.Add("PDTLVRNTMOBY", OracleDbType.Varchar2, 20).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;

                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage insertVariantDetail(VariantDetail variantDetail, String User)
        {
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSDTLVRNT.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PDTLVRNTVRNTID", OracleDbType.Int32).Value = variantDetail.VariantID;
                cmd.Parameters.Add("PDTLVRNTSZGID", OracleDbType.Varchar2, 20).Value = variantDetail.SizeGroup;
                cmd.Parameters.Add("PDTLVRNTSZID", OracleDbType.Varchar2, 20).Value = variantDetail.SizeDetail;
                cmd.Parameters.Add("PDTLVRNTCOGID", OracleDbType.Varchar2, 20).Value = variantDetail.ColorGroup;
                cmd.Parameters.Add("PDTLVRNTCOLID", OracleDbType.Varchar2, 20).Value = variantDetail.ColorDetail;
                cmd.Parameters.Add("PDTLVRNTSTGID", OracleDbType.Varchar2, 20).Value = variantDetail.StyleGroup;
                cmd.Parameters.Add("PDTLVRNTSTYLID", OracleDbType.Varchar2, 20).Value = variantDetail.StyleDetail;
                cmd.Parameters.Add("PDTLVRNTDINTF", OracleDbType.Varchar2, 1).Value = "0";
                cmd.Parameters.Add("PDTLVRNTCRBY", OracleDbType.Varchar2, 20).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;

                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage insertVariantMaster(VariantMaster variant, String User)
        {
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSMSTVRNT.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PVRNTITEMID", OracleDbType.Int32).Value = variant.ItemID;
                cmd.Parameters.Add("PVRNTSDESC", OracleDbType.Varchar2, 20).Value = variant.ShortDesc;
                cmd.Parameters.Add("PVRNTLDESC", OracleDbType.Varchar2, 50).Value = variant.LongDesc;
                cmd.Parameters.Add("PVRNTSTAT", OracleDbType.Int32).Value = variant.Status;
                cmd.Parameters.Add("PVRNTCRBY", OracleDbType.Varchar2, 20).Value = User;
                cmd.Parameters.Add("PVRNTDINTF", OracleDbType.Varchar2, 1).Value = "0";
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage insertAssortment(AssortmentMaster assortment, String User)
        {
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSASS.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("PSASSITEMID", OracleDbType.Int32).Value = assortment.ItemID;
                cmd.Parameters.Add("PSASSSITE", OracleDbType.Varchar2, 20).Value = assortment.Site;
                cmd.Parameters.Add("PSASSVRNTID", OracleDbType.Varchar2, 20).Value = assortment.VariantID;
                cmd.Parameters.Add("PSASSSTAT", OracleDbType.Int32).Value = assortment.Status;
                cmd.Parameters.Add("PSASSINTF", OracleDbType.Varchar2, 1).Value = "0";
                cmd.Parameters.Add("PSASSCRBY", OracleDbType.Varchar2, 20).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage deleteAssortment(AssortmentMaster assortment)
        {
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSASS.DEL_DATA";
                cmd.CommandType = CommandType.StoredProcedure;
                

                cmd.Parameters.Add("PSASSITEMID", OracleDbType.Int32).Value = assortment.ItemID;
                cmd.Parameters.Add("PSASSSITEID", OracleDbType.Varchar2, 20).Value = assortment.Site;
                cmd.Parameters.Add("PSASSVRNT", OracleDbType.Varchar2, 20).Value = assortment.VariantID;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                outputMsg.Message = e.Message;
                 outputMsg.Code = -99;
                return outputMsg;

            }
        }

        public OutputMessage updateAssortment(AssortmentMaster assortment, String User)
        {
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSASS.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PSASSITEMID", OracleDbType.Int32).Value = assortment.ItemID;
                cmd.Parameters.Add("PSASSSITE", OracleDbType.Varchar2, 20).Value = assortment.Site;
                cmd.Parameters.Add("PSASSVRNTID", OracleDbType.Varchar2, 20).Value = assortment.VariantID;
                cmd.Parameters.Add("PSASSSTAT", OracleDbType.Int32).Value = assortment.Status;
                cmd.Parameters.Add("PSASSINTF", OracleDbType.Varchar2, 1).Value = "0";
                cmd.Parameters.Add("PSASSMOBY", OracleDbType.Varchar2, 20).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage insertItemMaster(ItemMaster item, String User)
        {
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSMSTITEM.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PITEMITEMIDX", OracleDbType.Varchar2, 50).Value = item.ItemIDExternal;
                cmd.Parameters.Add("PITEMTYPE", OracleDbType.Int32).Value = item.Type;
                cmd.Parameters.Add("PITEMSDESC", OracleDbType.Varchar2, 50).Value = item.ShortDesc;
                cmd.Parameters.Add("PITEMLDESC", OracleDbType.Varchar2, 50).Value = item.LongDesc;
                cmd.Parameters.Add("PITEMBRNDID", OracleDbType.Varchar2, 50).Value = item.Brand;
                cmd.Parameters.Add("PITEMCRBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("PITEMINTF", OracleDbType.Varchar2, 50).Value = "0";
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage updateUser(User user, String CurrUser)
        {

            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSUSER.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PUSERUSID", OracleDbType.Varchar2, 50).Value = user.UserID;
                cmd.Parameters.Add("PUSERUSNM", OracleDbType.Varchar2, 50).Value = user.Username;
                cmd.Parameters.Add("PUSERPASW", OracleDbType.Varchar2, 50).Value = user.Password;
                cmd.Parameters.Add("PUSERUSDSC", OracleDbType.Varchar2, 50).Value = user.Description;
                cmd.Parameters.Add("PUSERSTAT", OracleDbType.Int32).Value = user.Status;
                cmd.Parameters.Add("PUSERSDAT", OracleDbType.Date).Value = user.StartDate;
                cmd.Parameters.Add("PUSEREDAT", OracleDbType.Date).Value = user.EndDate;
                cmd.Parameters.Add("PUSERTYPE", OracleDbType.Int32).Value = user.UserType;
                cmd.Parameters.Add("PUSERIMEI", OracleDbType.Varchar2, 50).Value = user.IMEI;
                cmd.Parameters.Add("PUSERACPROF", OracleDbType.Varchar2, 50).Value = user.AccessProfile;
                cmd.Parameters.Add("PUSERMEPROF", OracleDbType.Varchar2, 50).Value = user.MenuProfile;
                cmd.Parameters.Add("PUSERSTPROF", OracleDbType.Varchar2, 50).Value = user.SiteProfile;
                cmd.Parameters.Add("PUSERINTF", OracleDbType.Varchar2, 50).Value = "0";
                cmd.Parameters.Add("PUSERMOBY", OracleDbType.Varchar2, 50).Value = CurrUser;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage updateMenu(Menu menu, String CurrUser)
        {

            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSMENU.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PMENUMENUID", OracleDbType.Varchar2, 20).Value = menu.MenuID;
                cmd.Parameters.Add("PMENUMENUNM", OracleDbType.Varchar2, 50).Value = menu.MenuName;
                cmd.Parameters.Add("PMENUMEGRPID", OracleDbType.Varchar2, 20).Value = menu.MenuGroupID;
                cmd.Parameters.Add("PMENUMEGRPNM", OracleDbType.Varchar2, 50).Value = menu.MenuGroupName;
                cmd.Parameters.Add("PMENUMEURL", OracleDbType.Varchar2, 1000).Value = menu.MenuURL;
                cmd.Parameters.Add("PMENUINTF", OracleDbType.Varchar2, 1).Value = "0";
                cmd.Parameters.Add("PMENUMOBY", OracleDbType.Varchar2, 20).Value = CurrUser;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage updateSiteMaster(SiteMaster siteMaster, String CurrUser)
        {

            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSITE.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PSITESITE", OracleDbType.Varchar2, 50).Value = siteMaster.Site;
                cmd.Parameters.Add("PSITESCLAS", OracleDbType.Int32).Value = siteMaster.SiteClass;
                cmd.Parameters.Add("PSITESITENAME", OracleDbType.Varchar2, 50).Value = siteMaster.SiteName;
                cmd.Parameters.Add("PSITESITEINTF", OracleDbType.Varchar2, 1).Value = "0";
                cmd.Parameters.Add("PSITESITEMOBY", OracleDbType.Varchar2, 50).Value = CurrUser;
                cmd.Parameters.Add("PSITESITEFLAG", OracleDbType.Int32).Value = siteMaster.Enable;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage insertSiteMaster(SiteMaster siteMaster, String CurrUser)
        {

            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSITE.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PSITESITE", OracleDbType.Varchar2, 50).Value = siteMaster.Site;
                cmd.Parameters.Add("PSITESCLAS", OracleDbType.Int32).Value = siteMaster.SiteClass;
                cmd.Parameters.Add("PSITESITENAME", OracleDbType.Varchar2, 50).Value = siteMaster.SiteName;
                cmd.Parameters.Add("PSITESITEINTF", OracleDbType.Varchar2, 1).Value = "0";
                cmd.Parameters.Add("PSITESITECRBY", OracleDbType.Varchar2, 50).Value = CurrUser;
                cmd.Parameters.Add("PSITESITEFLAG", OracleDbType.Int32).Value = siteMaster.Enable;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage insertUser(User user, String CurrUser)
        {

            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSUSER.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PUSERUSID", OracleDbType.Varchar2, 20).Value = user.UserID;
                cmd.Parameters.Add("PUSERUSNM", OracleDbType.Varchar2, 20).Value = user.Username;
                cmd.Parameters.Add("PUSERPASW", OracleDbType.Varchar2, 20).Value = user.Password;
                cmd.Parameters.Add("PUSERUSDSC", OracleDbType.Varchar2, 50).Value = user.Description;
                cmd.Parameters.Add("PUSERSTAT", OracleDbType.Int32).Value = user.Status;
                cmd.Parameters.Add("PUSERSDAT", OracleDbType.Date).Value = user.StartDate;
                cmd.Parameters.Add("PUSEREDAT", OracleDbType.Date).Value = user.EndDate;
                cmd.Parameters.Add("PUSERTYPE", OracleDbType.Int32).Value = user.UserType;
                cmd.Parameters.Add("PUSERIMEI", OracleDbType.Varchar2, 20).Value = user.IMEI;
                cmd.Parameters.Add("PUSERACPROF", OracleDbType.Varchar2, 20).Value = user.AccessProfile;
                cmd.Parameters.Add("PUSERMEPROF", OracleDbType.Varchar2, 20).Value = user.MenuProfile;
                cmd.Parameters.Add("PUSERSTPROF", OracleDbType.Varchar2, 20).Value = user.SiteProfile;
                cmd.Parameters.Add("PUSERINTF", OracleDbType.Varchar2, 1).Value = "0";
                cmd.Parameters.Add("PUSERCRBY", OracleDbType.Varchar2, 20).Value = CurrUser;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;

                
                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();
                cmd.ExecuteNonQuery();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();


                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage updateParameterDetail(ParameterDetail parDetail, String User, String Copy)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSPARDTABLE.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("PPARDTABID", OracleDbType.Int32).Value = Int32.Parse(parDetail.ID);
                cmd.Parameters.Add("PPARDTABENT", OracleDbType.Int32).Value = Int32.Parse(parDetail.Entry);
                cmd.Parameters.Add("PPARDSDESC", OracleDbType.Varchar2, 10).Value = parDetail.ShortDescription;
                cmd.Parameters.Add("PPARDLDESC", OracleDbType.Varchar2, 20).Value = parDetail.LongDescription;
                cmd.Parameters.Add("PPARDSCLAS", OracleDbType.Int32).Value = Int32.Parse(parDetail.SiteClass);

                cmd.Parameters.Add("PPARDVAN1", OracleDbType.Int32).Value = parDetail.Number1.HasValue
                    ? parDetail.Number1
                    : (object)DBNull.Value;

                cmd.Parameters.Add("PPARDVAN2", OracleDbType.Int32).Value = parDetail.Number2.HasValue
                   ? parDetail.Number2
                   : (object)DBNull.Value;

                cmd.Parameters.Add("PPARDVAN3", OracleDbType.Int32).Value = parDetail.Number3.HasValue
                   ? parDetail.Number3
                   : (object)DBNull.Value;

                cmd.Parameters.Add("PPARDVAN4", OracleDbType.Int32).Value = parDetail.Number4.HasValue
                   ? parDetail.Number4
                   : (object)DBNull.Value;


                cmd.Parameters.Add("PPARDVAC1", OracleDbType.Varchar2, 20).Value = parDetail.Char1;
                cmd.Parameters.Add("PPARDVAC2", OracleDbType.Varchar2, 30).Value = parDetail.Char2;
                cmd.Parameters.Add("PPARDVAC3", OracleDbType.Varchar2, 50).Value = parDetail.Char3;

                cmd.Parameters.Add("PPARDDATE1", OracleDbType.Date).Value = parDetail.Date1.HasValue
                   ? parDetail.Date1
                   : (object)DBNull.Value;

                cmd.Parameters.Add("PPARDDATE2", OracleDbType.Date).Value = parDetail.Date2.HasValue
                   ? parDetail.Date2
                   : (object)DBNull.Value;

                cmd.Parameters.Add("PPARDDATE3", OracleDbType.Date).Value = parDetail.Date3.HasValue
                   ? parDetail.Date3
                   : (object)DBNull.Value;

                cmd.Parameters.Add("PPARDCOMM", OracleDbType.Varchar2, 1000).Value = parDetail.Comment;
                cmd.Parameters.Add("PPARDINTF", OracleDbType.Varchar2, 50).Value = "0";
                cmd.Parameters.Add("PPARDMOBY", OracleDbType.Varchar2, 20).Value = User;
                cmd.Parameters.Add("PCOPY", OracleDbType.Int32).Value = Copy;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage updateSiteProfileLinkManagementDetail(SiteProfileLink siteProfileLink, String User)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSPROFSITELINK.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;
                 


                cmd.Parameters.Add("PPRSTSTPROF", OracleDbType.Varchar2, 20).Value = siteProfileLink.SiteProfile;
                cmd.Parameters.Add("PPRSTSITE", OracleDbType.Varchar2, 20).Value = siteProfileLink.Site;
                cmd.Parameters.Add("PPRSTSDATE", OracleDbType.Date).Value = siteProfileLink.StartDate;
                cmd.Parameters.Add("PPRSTEDATE", OracleDbType.Date).Value = siteProfileLink.EndDate;
                cmd.Parameters.Add("PPRSTINTF", OracleDbType.Varchar2).Value = "0";
                cmd.Parameters.Add("PPRSTMOBY", OracleDbType.Varchar2, 20).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage insertSiteProfileLinkManagementDetail(SiteProfileLink siteProfileLink, String User)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSPROFSITELINK.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PPRSTSTPROF", OracleDbType.Varchar2, 20).Value = siteProfileLink.SiteProfile;
                cmd.Parameters.Add("PPRSTSITE", OracleDbType.Varchar2, 20).Value = siteProfileLink.Site;
                cmd.Parameters.Add("PPRSTSDATE", OracleDbType.Date).Value = siteProfileLink.StartDate;
                cmd.Parameters.Add("PPRSTEDATE", OracleDbType.Date).Value = siteProfileLink.EndDate;
                cmd.Parameters.Add("PPRSTINTF", OracleDbType.Varchar2).Value = "0";
                cmd.Parameters.Add("PPRSTCRBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage insertMenu(Menu menu, String User)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSMENU.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;
                

                cmd.Parameters.Add("PMENUMENUID", OracleDbType.Varchar2, 50).Value = menu.MenuID;
                cmd.Parameters.Add("PMENUMENUNM", OracleDbType.Varchar2, 50).Value = menu.MenuName;
                cmd.Parameters.Add("PMENUMEGRPID", OracleDbType.Varchar2, 50).Value = menu.MenuGroupID;
                cmd.Parameters.Add("PMENUMEGRPNM", OracleDbType.Varchar2, 50).Value = menu.MenuGroupName;
                cmd.Parameters.Add("PMENUMEURL", OracleDbType.Varchar2, 50).Value = menu.MenuURL;
                cmd.Parameters.Add("PMENUINTF", OracleDbType.Varchar2).Value = "0";
                cmd.Parameters.Add("PMENUCRBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage insertMenuProfileLinkManagementDetail(MenuProfileLink menuProfileLink, String User)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSMEPROFD.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;
                

                cmd.Parameters.Add("PMEPRDMEPROF", OracleDbType.Varchar2, 20).Value = menuProfileLink.MenuProfile;
                cmd.Parameters.Add("PMEPRDMENUID", OracleDbType.Varchar2, 20).Value = menuProfileLink.MenuID;
                cmd.Parameters.Add("PMEPRDCRBY", OracleDbType.Varchar2, 50).Value = User;
                cmd.Parameters.Add("PMEPRDINTF", OracleDbType.Int32).Value = 0;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage updateSiteProfile(SiteProfileHeader profileHeader, String CurrUser)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSITEPROF.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PSTPRSTPROF", OracleDbType.Varchar2, 20).Value = profileHeader.Profile;
                cmd.Parameters.Add("PSTPRSTDESC", OracleDbType.Varchar2, 50).Value = profileHeader.Description;
                cmd.Parameters.Add("PSTPRINTF", OracleDbType.Varchar2, 1).Value = "0";
                cmd.Parameters.Add("PSTPRMOBY", OracleDbType.Varchar2, 20).Value = CurrUser;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage updateMenuProfile(MenuProfileHeader menuHeader, String CurrUser)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSMEPROFH.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PMEPRHMEPROF", OracleDbType.Varchar2, 20).Value = menuHeader.Profile;
                cmd.Parameters.Add("PMEPRHMEDESC", OracleDbType.Varchar2, 50).Value = menuHeader.Description;
                cmd.Parameters.Add("PMEPRHMOBY", OracleDbType.Varchar2, 20).Value = CurrUser;
                cmd.Parameters.Add("PMEPRHINTF", OracleDbType.Varchar2, 1).Value = "0";
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage updateIntSite(SiteMaster Site, String ROWID, String CurrUser)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                //PINTSITESITE VARCHAR2,
                //    PINTSITESCLAS   NUMBER,
                //    PINTSITESITENAME VARCHAR2,
                //    PINTSITESITEINTF    NUMBER,
                //    PINTSITESITEMOBY VARCHAR2,
                //    PINTSITESITEFLAG    NUMBER,
                //    PINTSITEROWID VARCHAR2,

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSINTSITE.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PINTSITESITE", OracleDbType.Varchar2, 20).Value = Site.Site;
                cmd.Parameters.Add("PINTSITESCLAS", OracleDbType.Int32, 1).Value = Site.SiteClass;
                cmd.Parameters.Add("PINTSITESITENAME", OracleDbType.Varchar2, 50).Value = Site.SiteName;
                cmd.Parameters.Add("PINTSITESITEMOBY", OracleDbType.Varchar2, 20).Value = CurrUser;
                cmd.Parameters.Add("PINTSITESITEFLAG", OracleDbType.Int32).Value = "0";
                cmd.Parameters.Add("PINTSITEROWID", OracleDbType.Varchar2).Value = ROWID;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("updateIntSite Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage updateIntVariant(VariantDetail VariantDtl, String ROWID, String CurrUser)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                //PINTDTLVRNTVRNTID NUMBER,
                //PINTDTLVRNTITMID VARCHAR2,
                //    PINTDTLVRNTSZGID VARCHAR2,
                //    PINTDTLVRNTSZID VARCHAR2,
                //    PINTDTLVRNTCOGID VARCHAR2,
                //    PINTDTLVRNTCOLID VARCHAR2,
                //    PINTDTLVRNTSTGID VARCHAR2,
                //    PINTDTLVRNTSTYLID VARCHAR2,
                //    PINTDTLVRNTMOBY VARCHAR2,
                //    PINTDTLVRNTROWID VARCHAR2,
                //    POUTRSNCODE OUT NUMBER,
                //    POUTRSNMSG OUT VARCHAR2

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSINTDTLVRNT.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PINTDTLVRNTVRNTID", OracleDbType.Int32, 10).Value = VariantDtl.VariantID;
                cmd.Parameters.Add("PINTDTLVRNTITMID", OracleDbType.Varchar2, 30).Value = VariantDtl.ItemID;
                cmd.Parameters.Add("PINTDTLVRNTSZGID", OracleDbType.Varchar2, 20).Value = VariantDtl.SizeGroup;
                cmd.Parameters.Add("PINTDTLVRNTSZID", OracleDbType.Varchar2, 20).Value = VariantDtl.SizeDetail;
                cmd.Parameters.Add("PINTDTLVRNTCOGID", OracleDbType.Varchar2, 20).Value = VariantDtl.ColorGroup;
                cmd.Parameters.Add("PINTDTLVRNTCOLID", OracleDbType.Varchar2, 20).Value = VariantDtl.ColorDetail;
                cmd.Parameters.Add("PINTDTLVRNTSTGID", OracleDbType.Varchar2, 20).Value = VariantDtl.StyleGroup;
                cmd.Parameters.Add("PINTDTLVRNTSTYLID", OracleDbType.Varchar2, 20).Value = VariantDtl.StyleDetail;
                cmd.Parameters.Add("PINTDTLVRNTMOBY", OracleDbType.Varchar2, 20).Value = CurrUser;
                cmd.Parameters.Add("PINTDTLVRNTROWID", OracleDbType.Varchar2).Value = ROWID;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("updateIntSite Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }
        public OutputMessage updateIntDN(DeliveryNote DN, String ROWID)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                //PCMSID NUMBER,
                //     PCMSITEMID  VARCHAR2,
                //    PCMSBARCODE VARCHAR2,
                //    PCMSDESC    VARCHAR2,
                //    PCMSQTY NUMBER,
                //    PCMSPRICE   NUMBER,
                //    PCMSSTORE VARCHAR2,
                //    PCMSUSERID  VARCHAR2,
                //    PCMSDATE DATE,
                //    PCMSDNROWID VARCHAR,

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSDN_INT.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PCMSID", OracleDbType.Int32).Value = DN.CMSId;
                cmd.Parameters.Add("PCMSITEMID", OracleDbType.Int32, 50).Value = DN.ItemID;
                cmd.Parameters.Add("PCMSBARCODE", OracleDbType.Varchar2, 50).Value = DN.Barcode;
                cmd.Parameters.Add("PCMSDESC", OracleDbType.Varchar2, 250).Value = DN.Description;
                cmd.Parameters.Add("PCMSQTY", OracleDbType.Int32, 18).Value = DN.Qty;
                cmd.Parameters.Add("PCMSPRICE", OracleDbType.Int32, 18).Value = DN.Price;
                cmd.Parameters.Add("PCMSSTORE", OracleDbType.Varchar2, 100).Value = DN.Store;
                cmd.Parameters.Add("PCMSUSERID", OracleDbType.Varchar2, 100).Value = DN.UserID;
                cmd.Parameters.Add("PCMSDATE", OracleDbType.Date).Value = DN.Date;
                cmd.Parameters.Add("PCMSDNROWID", OracleDbType.Varchar2).Value = ROWID;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("updateIntDN Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage updateIntBarcode(BarcodeMaster Barcode, String ROWID, String CurrUser)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                //PINTBRCDBRCDID VARCHAR,
                //     PINTBRCDITEMID NUMBER,
                //    PINTBRCDVRNTID NUMBER,
                //    PINTBRCDTYPE NUMBER,
                //    PINTBRCDSTAT NUMBER,
                //    PINTBRCDSDAT DATE,
                //    PINTBRCDEDAT DATE,
                //    PINTBRCDMOBY VARCHAR,  
                //    PINTBRCDROWID VARCHAR2,

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSINTMSTBRCD.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PINTBRCDBRCDID", OracleDbType.Varchar2, 20).Value = Barcode.Barcode;
                cmd.Parameters.Add("PINTBRCDITEMID", OracleDbType.Varchar2, 30).Value = Barcode.ItemID;
                cmd.Parameters.Add("PINTBRCDVRNTID", OracleDbType.Int32, 10).Value = Barcode.VariantID;
                cmd.Parameters.Add("PINTBRCDTYPE", OracleDbType.Int32, 1).Value = Barcode.Type;
                cmd.Parameters.Add("PINTBRCDSTAT", OracleDbType.Int32, 1).Value = Barcode.Status;
                cmd.Parameters.Add("PINTBRCDSDAT", OracleDbType.Date).Value = Barcode.StartDate;
                cmd.Parameters.Add("PINTBRCDEDAT", OracleDbType.Date).Value = Barcode.EndDate;
                cmd.Parameters.Add("PINTBRCDMOBY", OracleDbType.Varchar2, 20).Value = CurrUser;
                cmd.Parameters.Add("PINTBRCDROWID", OracleDbType.Varchar2).Value = ROWID;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("updateIntBarcode Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        

        public OutputMessage updateIntPriceAssortment(PriceGroup Price, String ROWID, String CurrUser)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                //PINTSPRCITEMIDX VARCHAR2,
                //     PINTSPRCDVRNTIDX VARCHAR2, 
                //    PINTSPRCSITE VARCHAR2,
                //    PINTSPRCSPRICE NUMBER,
                //    PINTSPRCVAT NUMBER,
                //    PINTSPRCSDAT DATE,
                //    PINTSPRCEDAT DATE,
                //    PINTSPRCMOBY VARCHAR,  
                //    PINTSPRCROWID VARCHAR2,

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSINTSPRICEASS.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PINTSPRCITEMIDX", OracleDbType.Varchar2, 20).Value = Price.ItemID;
                cmd.Parameters.Add("PINTSPRCDVRNTIDX", OracleDbType.Varchar2, 20).Value = Price.VariantID;
                cmd.Parameters.Add("PINTSPRCSITE", OracleDbType.Varchar2, 20).Value = Price.Site;
                cmd.Parameters.Add("PINTSPRCSPRICE", OracleDbType.Int32, 12).Value = Price.Price;
                cmd.Parameters.Add("PINTSPRCVAT", OracleDbType.Int32, 2).Value = Price.VAT;
                cmd.Parameters.Add("PINTSPRCSDAT", OracleDbType.Date).Value = Price.SDate;
                cmd.Parameters.Add("PINTSPRCEDAT", OracleDbType.Date).Value = Price.Edate;
                cmd.Parameters.Add("PINTSPRCMOBY", OracleDbType.Varchar2, 20).Value = CurrUser;
                cmd.Parameters.Add("PINTASSSTAT", OracleDbType.Int32).Value = Price.AssortmentStatus;
                cmd.Parameters.Add("PINTSPRCROWID", OracleDbType.Varchar2).Value = ROWID;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("updateIntPriceAssortment Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage updateIntItem(ItemMaster Item, String ROWID, String CurrUser)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                //PINTITEMITEMIDX VARCHAR2,
                //    PINTITEMTYPE NUMBER,
                //    PINTITEMSDESC VARCHAR,
                //    PINTITEMLDESC VARCHAR,
                //    PINTITEMBRNDID VARCHAR,
                //    PINTITEMBRNDDESC VARCHAR,
                //    PINTUTIL VARCHAR,
                //    PINTITEMROWID VARCHAR,
                //    POUTRSNCODE OUT NUMBER,
                //    POUTRSNMSG OUT VARCHAR2);

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSINTMSTITEM.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PINTITEMITEMIDX", OracleDbType.Varchar2, 30).Value = Item.ItemIDExternal;
                cmd.Parameters.Add("PINTITEMTYPE", OracleDbType.Int32, 2).Value = Item.Type;
                cmd.Parameters.Add("PINTITEMSDESC", OracleDbType.Varchar2, 20).Value = Item.ShortDesc;
                cmd.Parameters.Add("PINTITEMLDESC", OracleDbType.Varchar2, 50).Value = Item.LongDesc;
                cmd.Parameters.Add("PINTITEMBRNDID", OracleDbType.Varchar2, 3).Value = Item.Brand;
                //cmd.Parameters.Add("PINTITEMBRNDDESC", OracleDbType.Varchar2, 50).Value = "0";
                cmd.Parameters.Add("PINTITEMMOBY", OracleDbType.Varchar2, 20).Value = CurrUser;
                cmd.Parameters.Add("PINTITEMROWID", OracleDbType.Varchar2).Value = ROWID;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("updateIntSite Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage resetIntSite(String ROWID, String CurrUser)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                //procedure RESET_DATA(PINTSITEROWID VARCHAR2,
                //    PINTSITESITEMOBY VARCHAR2,
                //    POUTRSNCODE OUT NUMBER,
                //    POUTRSNMSG OUT VARCHAR2);

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSINTSITE.RESET_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PINTSITEROWID", OracleDbType.Varchar2).Value = ROWID;
                cmd.Parameters.Add("PINTSITESITEMOBY", OracleDbType.Varchar2, 20).Value = CurrUser;

                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("resetIntSite Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage resetIntVariant(String ROWID, String CurrUser)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                //procedure RESET_DATA(PINTSITEROWID VARCHAR2,
                //    PINTSITESITEMOBY VARCHAR2,
                //    POUTRSNCODE OUT NUMBER,
                //    POUTRSNMSG OUT VARCHAR2);

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSINTDTLVRNT.RESET_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PINTDTLVRNTROWID", OracleDbType.Varchar2).Value = ROWID;
                cmd.Parameters.Add("PINTDTLVRNTMOBY", OracleDbType.Varchar2, 20).Value = CurrUser;

                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("resetIntVariant Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage resetIntDN(String ROWID)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                //procedure RESET_DATA(PINTSITEROWID VARCHAR2,
                //    PINTSITESITEMOBY VARCHAR2,
                //    POUTRSNCODE OUT NUMBER,
                //    POUTRSNMSG OUT VARCHAR2);

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSDN_INT.RESET_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PCMSDNROWID", OracleDbType.Varchar2).Value = ROWID;

                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("resetIntDN Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage resetIntPriceAssortment(String ROWID, String CurrUser)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                //procedure RESET_DATA(PINTSITEROWID VARCHAR2,
                //    PINTSITESITEMOBY VARCHAR2,
                //    POUTRSNCODE OUT NUMBER,
                //    POUTRSNMSG OUT VARCHAR2);

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSINTSPRICEASS.RESET_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PINTSPRCROWID", OracleDbType.Varchar2).Value = ROWID;
                cmd.Parameters.Add("PINTSPRCMOBY", OracleDbType.Varchar2, 20).Value = CurrUser;

                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("resetIntVariant Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage resetIntBarcode(String ROWID, String CurrUser)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                //procedure RESET_DATA(PINTSITEROWID VARCHAR2,
                //    PINTSITESITEMOBY VARCHAR2,
                //    POUTRSNCODE OUT NUMBER,
                //    POUTRSNMSG OUT VARCHAR2);

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSINTMSTBRCD.RESET_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PINTBRCDROWID", OracleDbType.Varchar2).Value = ROWID;
                cmd.Parameters.Add("PINTBRCDMOBY", OracleDbType.Varchar2, 20).Value = CurrUser;

                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("resetIntBarcode Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        

        public OutputMessage resetIntItem(String ROWID, String CurrUser)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                //procedure RESET_DATA(PINTSITEROWID VARCHAR2,
                //    PINTSITESITEMOBY VARCHAR2,
                //    POUTRSNCODE OUT NUMBER,
                //    POUTRSNMSG OUT VARCHAR2);

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSINTMSTITEM.RESET_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PINTITEMROWID", OracleDbType.Varchar2).Value = ROWID;
                cmd.Parameters.Add("PINTITEMMOBY", OracleDbType.Varchar2, 20).Value = CurrUser;

                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("resetIntItem Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage updateAccessProfile(AccessProfileHeader accessHeader, String CurrUser)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSACCPROFH.UPD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PACPRHACPROF", OracleDbType.Varchar2, 20).Value = accessHeader.Profile;
                cmd.Parameters.Add("PACPRHACDESC", OracleDbType.Varchar2, 50).Value = accessHeader.Description;
                cmd.Parameters.Add("PACPRHINTF", OracleDbType.Varchar2, 1).Value = "0";
                cmd.Parameters.Add("PACPRHMOBY", OracleDbType.Varchar2, 20).Value = CurrUser;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage insertSiteProfile(SiteProfileHeader profileHeader, String CurrUser)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSITEPROF.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PSTPRSTPROF", OracleDbType.Varchar2, 50).Value = profileHeader.Profile;
                cmd.Parameters.Add("PSTPRSTDESC", OracleDbType.Varchar2, 50).Value = profileHeader.Description;
                cmd.Parameters.Add("PSTPRINTF", OracleDbType.Varchar2, 50).Value = "0";
                cmd.Parameters.Add("PSTPRCRBY", OracleDbType.Varchar2, 50).Value = CurrUser;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage insertMenuProfile(MenuProfileHeader menuHeader, String CurrUser)
        {


            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSMEPROFH.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("PMEPRHMEPROF", OracleDbType.Varchar2, 50).Value = menuHeader.Profile;
                cmd.Parameters.Add("PMEPRHMEDESC", OracleDbType.Varchar2, 50).Value = menuHeader.Description;
                cmd.Parameters.Add("PMEPRHCRBY", OracleDbType.Varchar2, 50).Value = CurrUser;
                cmd.Parameters.Add("PMEPRHINTF", OracleDbType.Varchar2, 50).Value = "0";
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage insertAccessProfile(AccessProfileHeader accessHeader, String CurrUser, String SiteClass)
        {
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSACCPROFH.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

              

                cmd.Parameters.Add("PACPRHACPROF", OracleDbType.Varchar2, 50).Value = accessHeader.Profile;
                cmd.Parameters.Add("PACPRHACDESC", OracleDbType.Varchar2, 50).Value = accessHeader.Description;
                cmd.Parameters.Add("PACPRHINTF", OracleDbType.Varchar2, 50).Value = "0";
                cmd.Parameters.Add("PACPRHCRBY", OracleDbType.Varchar2, 50).Value = CurrUser;
                cmd.Parameters.Add("PSCLAS", OracleDbType.Int32).Value = SiteClass;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public OutputMessage ProcessAccessProfileDetail(AccessProfileDetail accessDetail, String CurrUser)
        {
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSACCPROFD.PROCESS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;

                

                cmd.Parameters.Add("PACPRDACPROF", OracleDbType.Varchar2, 50).Value = accessDetail.Profile;
                cmd.Parameters.Add("PACPRDMENUID", OracleDbType.Varchar2, 50).Value = accessDetail.MenuId;
                cmd.Parameters.Add("PACPRDFUNCID", OracleDbType.Int32).Value = accessDetail.FunctionId;
                cmd.Parameters.Add("PACPRDACTYPE", OracleDbType.Int32).Value = accessDetail.Type;
                cmd.Parameters.Add("PACPRHINTF", OracleDbType.Varchar2, 50).Value = "0";
                cmd.Parameters.Add("PACPRHCRBY", OracleDbType.Varchar2, 50).Value = CurrUser;
                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();                
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public string DeleteItemMasterByID(String ID)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "DELETE FROM KDSMASTERITEMCMSV3 WHERE " +
                                  "ID = :Id";

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":Id", OracleDbType.Varchar2)).Value = ID;

                logger.Debug(cmd.CommandText);

                cmd.ExecuteNonQuery();

                this.Close();
                ResultString = "Success";
                return ResultString;
            }
            catch (Exception e)
            {
                logger.Error("DeleteItemMaster Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }





        public string insertSiteByProfileID(String SiteID, String ProfileID, String UserID)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "INSERT INTO KDSPROFILESITELINKCMSV3 (SITEID, PROFILEID, CREATEDBY, CREATEDDATE) VALUES (:SiteId, :ProfileId, :UserId, SYSDATE)";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":SiteId", OracleDbType.Varchar2)).Value = SiteID;
                cmd.Parameters.Add(new OracleParameter(":ProfileId", OracleDbType.Varchar2)).Value = ProfileID;
                cmd.Parameters.Add(new OracleParameter(":UserId", OracleDbType.Varchar2)).Value = UserID;

                logger.Debug(cmd.CommandText);

                cmd.ExecuteNonQuery();

                this.Close();
                ResultString = "Success";
                return ResultString;
            }
            catch (Exception e)
            {
                logger.Error("insertSiteByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public string deleteSiteByProfileID(String SiteID, String ProfileID)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "DELETE FROM KDSPROFILESITELINKCMSV3 WHERE SITEID = :SiteId AND Profileid = :ProfileId";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":SiteId", OracleDbType.Varchar2)).Value = SiteID;
                cmd.Parameters.Add(new OracleParameter(":ProfileId", OracleDbType.Varchar2)).Value = ProfileID;

                logger.Debug(cmd.CommandText);

                cmd.ExecuteNonQuery();

                this.Close();
                ResultString = "Success";
                return ResultString;
            }
            catch (Exception e)
            {
                logger.Error("deleteSiteByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public string deleteMenuByProfileID(String MenuID, String ProfileID)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "DELETE FROM KDSPROFILEMENULINKCMSV3 WHERE MENUID = :MenuId AND Profileid = :ProfileId";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":MenuId", OracleDbType.Varchar2)).Value = MenuID;
                cmd.Parameters.Add(new OracleParameter(":ProfileId", OracleDbType.Varchar2)).Value = ProfileID;

                logger.Debug(cmd.CommandText);

                cmd.ExecuteNonQuery();

                this.Close();
                ResultString = "Success";
                return ResultString;
            }
            catch (Exception e)
            {
                logger.Error("deleteMenuByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public string GetDuplicateProfileName(String ProfileName)
        {
            try
            {

                this.Connect();
                OracleCommand cmd = new OracleCommand();

                String Name = "";
                cmd.Connection = con;

                cmd.CommandText = "SELECT PROFILENAME FROM KDSPROFILECMSV3 WHERE PROFILENAME = :ProfileName";
                cmd.Parameters.Add(new OracleParameter(":ProfileName", OracleDbType.Varchar2)).Value = ProfileName;


                logger.Debug(cmd.CommandText);


                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Name = dr["PROFILENAME"].ToString(); ;

                }


                this.Close();
                return Name;
            }
            catch (Exception e)
            {
                logger.Error("GetDuplicateProfileName Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public string GetValidSiteCount()
        {
            try
            {

                this.Connect();
                OracleCommand cmd = new OracleCommand();

                String Name = "";
                cmd.Connection = con;

                cmd.CommandText = "select count(1) as total from kdscmssite " +
                                  "where sitesitestatus = 1 " +
                                  "and sitesiteflag = 1 ";


                logger.Debug(cmd.CommandText);


                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Name = dr["total"].ToString(); ;

                }


                this.Close();
                return Name;
            }
            catch (Exception e)
            {
                logger.Error("GetValidSiteCount Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public string insertProfile(String ProfileName, String UserID)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();

                String ProfileID_nextVal = "";
                cmd.Connection = con;

                cmd.CommandText = "select KDSPROFILECMSV3_SEQ.NEXTVAL as NEXTVAL from dual";
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ProfileID_nextVal = dr["NEXTVAL"].ToString(); ;

                }

                cmd.CommandText = "INSERT INTO KDSPROFILECMSV3 (PROFILEID, PROFILENAME, CREATEDBY, CREATEDDATE) " +
                                    "VALUES (:ProfileId, :ProfileName , :UserId, SYSDATE)";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter(":ProfileId", OracleDbType.Varchar2)).Value = ProfileID_nextVal;
                cmd.Parameters.Add(new OracleParameter(":ProfileName", OracleDbType.Varchar2)).Value = ProfileName;
                cmd.Parameters.Add(new OracleParameter(":UserId", OracleDbType.Varchar2)).Value = UserID;


                logger.Debug(cmd.CommandText);

                cmd.ExecuteNonQuery();
                this.Close();
                ResultString = "Success";
                return ResultString;
            }
            catch (Exception e)
            {
                logger.Error("insertProfile Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public DataTable GetAllSalesHeader(DateTime? DateFrom, DateTime? DateTo, String Site, String SiteProfile)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;



                cmd.CommandText = "select " +
                                  "SLSHSLID as \"Sales ID\", " +
                                  "SLSHSLNOTA as \"Nomor Nota\", " +
                                  "SLSHRCPTID as \"Receipt ID\", " +
                                  "SLSHSLDATE as \"Transaction Date\", " +
                                  "SLSHSITE as \"Site\" " +
                                  "from kdscmsslsh " +
                                  "where " +
                                  "exists " +
                                  "(select 1 from KDSCMSPROFSITELINK where PRSTSTPROF = :Siteprofile and PRSTSITE = kdscmsslsh.SLSHSITE) ";

                cmd.Parameters.Add(new OracleParameter(":Siteprofile", OracleDbType.Varchar2)).Value = SiteProfile;

                cmd.CommandType = CommandType.Text;


                if (DateFrom.HasValue && DateTo.HasValue)
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SLSHSLDATE between :FromDate and :ToDate ";
                    cmd.Parameters.Add(new OracleParameter(":FromDate", OracleDbType.Date)).Value = DateFrom;
                    cmd.Parameters.Add(new OracleParameter(":ToDate", OracleDbType.Date)).Value = DateTo;
                }

                if (!string.IsNullOrWhiteSpace(Site))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SLSHSITE = :Site ";
                    cmd.Parameters.Add(new OracleParameter(":Site", OracleDbType.Varchar2)).Value = Site;
                }

                OracleDataReader dr = cmd.ExecuteReader();

                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        public SalesSummary GetSalesSummary(SalesSummary salesSummary, String SiteProfile, String Type)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select " +
                                  "count(SLSDSLNOTA) as TotalNota, " +
                                  "count(SLSDBRCD) as TotalItem, " +
                                  "sum(SLSDSLQTY) as TotalQuantity, " +
                                  "sum(SLSDTOTPRC) as TotalPrice, " +
                                  "sum(SLSDDISCTOT) as TotalDiscount, " +
                                  "sum(SLSDSLTOTCUS) as TotalSales " +
                                  "from KDSCMSSLSD " +
                                  "where exists " +
                                  "(select 1 from KDSCMSPROFSITELINK where PRSTSTPROF = :SiteProfile and PRSTSITE = KDSCMSSLSD.SLSDSITE) " +
                                  "and " +
                                  "    decode(:pType, 1,1,:pType1) = " +
                                  "    decode(:pType2, 1, 1, " +
                                  "    DECODE((SLSDSLQTY / abs(SLSDSLQTY)), -1, 3 " +
                                  "                        , 1, 2)) ";

                cmd.Parameters.Add(new OracleParameter(":SiteProfile", OracleDbType.Varchar2)).Value = SiteProfile;
                cmd.Parameters.Add(new OracleParameter(":pType", OracleDbType.Varchar2)).Value = Type;
                cmd.Parameters.Add(new OracleParameter(":pType1", OracleDbType.Varchar2)).Value = Type;
                cmd.Parameters.Add(new OracleParameter(":pType2", OracleDbType.Varchar2)).Value = Type;

                cmd.CommandType = CommandType.Text;


                if (salesSummary.FromDate.HasValue && salesSummary.ToDate.HasValue)
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SLSDSLDATE between :FromDate and :ToDate ";
                    cmd.Parameters.Add(new OracleParameter(":FromDate", OracleDbType.Date)).Value = salesSummary.FromDate;
                    cmd.Parameters.Add(new OracleParameter(":ToDate", OracleDbType.Date)).Value = salesSummary.ToDate;
                }

                if (!string.IsNullOrWhiteSpace(salesSummary.ReceiptID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SLSDRCPTID = :Receipt ";
                    cmd.Parameters.Add(new OracleParameter(":Receipt", OracleDbType.Varchar2)).Value = salesSummary.ReceiptID;
                }

                if (!string.IsNullOrWhiteSpace(salesSummary.Nota))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SLSDSLNOTA = :Nota ";
                    cmd.Parameters.Add(new OracleParameter(":Nota", OracleDbType.Varchar2)).Value = salesSummary.Nota;
                }

                if (!string.IsNullOrWhiteSpace(salesSummary.Site))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SLSDSITE = :Site ";
                    cmd.Parameters.Add(new OracleParameter(":Site", OracleDbType.Varchar2)).Value = salesSummary.Site;
                }



                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    salesSummary.TotItem = String.IsNullOrWhiteSpace(dr["TotalItem"].ToString()) ? 0 : Int32.Parse(dr["TotalItem"].ToString());
                    salesSummary.TotPrice = String.IsNullOrWhiteSpace(dr["TotalPrice"].ToString()) ? 0 : Int32.Parse(dr["TotalPrice"].ToString());
                    salesSummary.TotDisc = String.IsNullOrWhiteSpace(dr["TotalDiscount"].ToString()) ? 0 : Int32.Parse(dr["TotalDiscount"].ToString());
                    salesSummary.TotSales = String.IsNullOrWhiteSpace(dr["TotalSales"].ToString()) ? 0 : Int32.Parse(dr["TotalSales"].ToString());
                    salesSummary.TotQty = String.IsNullOrWhiteSpace(dr["TotalQuantity"].ToString()) ? 0 : Int32.Parse(dr["TotalQuantity"].ToString());
                    salesSummary.TotNota = String.IsNullOrWhiteSpace(dr["TotalNota"].ToString()) ? 0 : Int32.Parse(dr["TotalNota"].ToString());

                }

                this.Close();
                return salesSummary;
            }
            catch (Exception e)
            {
                logger.Error("GetSalesSummary Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }


        public DataTable GetSalesDetail(String SalesID, String ReceiptID, String Nota)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;



                cmd.CommandText = "select " +
                                  "SLSDSITE as \"Site\", " +
                                  "SLSDSLDATE as \"Sales Date\", " +
                                  "SLSDSLNOTA as \"Nota\", " +
                                  "SLSDBRCD as \"Barcode\", " +
                                  "SLSDSLQTY as \"Quantity\", " +
                                  "SLSDDISCTOT as \"Total Discount\", " +
                                  "SLSDSLTOTCUS as \"Total Sales\", " +
                                  "SLSDCOMM as \"Comment\" " +
                                  "from KDSCMSSLSD " +
                                  "where SLSDSLID = :SalesID " +
                                  "and SLSDRCPTID = :Receipt " +
                                  "and SLSDSLNOTA = :Nota " +
                                  "order by SLSDLNNUM";


                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new OracleParameter(":SalesID", OracleDbType.Varchar2)).Value = SalesID;
                cmd.Parameters.Add(new OracleParameter(":Receipt", OracleDbType.Varchar2)).Value = ReceiptID;
                cmd.Parameters.Add(new OracleParameter(":Nota", OracleDbType.Varchar2)).Value = Nota;



                OracleDataReader dr = cmd.ExecuteReader();

                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetSalesDetail2(DateTime? DateFrom, DateTime? DateTo, String Site, String SiteProfile, String Type)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;


                cmd.CommandText = "select " +
                                  "SLSDSITE as \"Site\", " +
                                  "SLSDSLDATE as \"Sales Date\", " +
                                  "SLSDSLNOTA as \"Nota\", " +
                                  "SLSDBRCD as \"Barcode\", " +
                                  "SLSDSLQTY as \"Quantity\", " +
                                  "SLSDDISCTOT as \"Total Discount\",  " +
                                  "SLSDSLTOTCUS as \"Total Sales\",  " +
                                  "SLSDCOMM as \"Comment\",  " +
                                  "DECODE((SLSDSLQTY / abs(SLSDSLQTY)), -1, 'Retur' " +
                                  "                        , 1, 'Sales') as \"Type\" " +
                                  "from KDSCMSSLSD " +
                                  "where " +
                                  "exists(select 1 from KDSCMSPROFSITELINK where PRSTSTPROF = :SiteProfile and PRSTSITE = KDSCMSSLSD.SLSDSITE)  " +
                                  "and " +
                                  "    decode(:pType, 1,1,:pType1) = " +
                                  "    decode(:pType2, 1, 1, " +
                                  "    DECODE((SLSDSLQTY / abs(SLSDSLQTY)), -1, 3 " +
                                  "                        , 1, 2)) ";


                cmd.Parameters.Add(new OracleParameter(":SiteProfile", OracleDbType.Varchar2)).Value = SiteProfile;
                cmd.Parameters.Add(new OracleParameter(":pType", OracleDbType.Varchar2)).Value = Type;
                cmd.Parameters.Add(new OracleParameter(":pType1", OracleDbType.Varchar2)).Value = Type;
                cmd.Parameters.Add(new OracleParameter(":pType2", OracleDbType.Varchar2)).Value = Type;
                cmd.CommandType = CommandType.Text;


                if (DateFrom.HasValue && DateTo.HasValue)
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SLSDSLDATE between :FromDate and :ToDate ";
                    cmd.Parameters.Add(new OracleParameter(":FromDate", OracleDbType.Date)).Value = DateFrom;
                    cmd.Parameters.Add(new OracleParameter(":ToDate", OracleDbType.Date)).Value = DateTo;
                }

                if (!string.IsNullOrWhiteSpace(Site))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SLSDSITE = :Site ";
                    cmd.Parameters.Add(new OracleParameter(":Site", OracleDbType.Varchar2)).Value = Site;
                }

                OracleDataReader dr = cmd.ExecuteReader();

                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public DataTable GetSalesDetailAll(SalesDetail SalesDet, String SiteProfile)
        {
            try
            {
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;



                cmd.CommandText = "SELECT " +
                                  "SLSDSLNOTA, " +
                                  "SLSDRCPTID, " +
                                  "SLSDLNNUM, " +
                                  "SLSDSLDATE, " +
                                  "SLSDSITE, " +
                                  "SLSDITEMID, " +
                                  "SLSDBRCD, " +
                                  "SLSDSLQTY, " +
                                  "SLSDSKUID, " +
                                  "SLSDSLPRC, " +
                                  "SLSDTOTPRC, " +
                                  "SLSDDISCTOT, " +
                                  "SLSDSLTOT " +
                                  "from KDSCMSSLSD " +
                                  "WHERE EXISTS(select 1 from KDSCMSPROFSITELINK where PRSTSTPROF = :Siteprofile and PRSTSITE = KDSCMSSLSD.SLSDSITE) ";


                cmd.Parameters.Add(new OracleParameter(":Siteprofile", OracleDbType.Varchar2)).Value = SiteProfile;

                cmd.CommandType = CommandType.Text;

                if (SalesDet.FromDate.HasValue && SalesDet.ToDate.HasValue)
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SLSDSLDATE between :FromDate and :ToDate ";
                    cmd.Parameters.Add(new OracleParameter(":FromDate", OracleDbType.Date)).Value = SalesDet.FromDate;
                    cmd.Parameters.Add(new OracleParameter(":ToDate", OracleDbType.Date)).Value = SalesDet.ToDate;
                }

                if (!string.IsNullOrWhiteSpace(SalesDet.Nota))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SLSDSLNOTA = :Nota ";
                    cmd.Parameters.Add(new OracleParameter(":Nota", OracleDbType.Varchar2)).Value = SalesDet.Nota;
                }

                if (!string.IsNullOrWhiteSpace(SalesDet.ReceiptID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SLSDRCPTID = :Receipt ";
                    cmd.Parameters.Add(new OracleParameter(":Receipt", OracleDbType.Varchar2)).Value = SalesDet.ReceiptID;
                }

                if (!string.IsNullOrWhiteSpace(SalesDet.ItemID))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SLSDITEMID = :ItemID ";
                    cmd.Parameters.Add(new OracleParameter(":ItemID", OracleDbType.Varchar2)).Value = SalesDet.ItemID;
                }

                if (!string.IsNullOrWhiteSpace(SalesDet.Barcode))
                {
                    cmd.CommandText = cmd.CommandText +
                                      "and SLSDBRCD = :Barcode ";
                    cmd.Parameters.Add(new OracleParameter(":Barcode", OracleDbType.Varchar2)).Value = SalesDet.Barcode;
                }

                OracleDataReader dr = cmd.ExecuteReader();
                DataTable DT = new DataTable();
                DT.Load(dr);
                this.Close();
                return DT;
            }
            catch (Exception e)
            {
                logger.Error("GetSiteDataByProfileID Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }


        public String GetSiteCodeandNameFromSiteCode(String Site)
        {
            try
            {
                String SiteCodeAndName = null;
                this.Connect();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select SITESITE || ' ' || SITESITENAME as SITE " +
                                  "from KDSCMSSITE " +
                                  "where SITESITE = :Site";

                cmd.Parameters.Add(new OracleParameter(":Site", OracleDbType.Varchar2)).Value = Site;

                cmd.CommandType = CommandType.Text;


                logger.Debug(cmd.CommandText);

                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    SiteCodeAndName = dr["Site"].ToString();
                }

                this.Close();
                return SiteCodeAndName;
            }
            catch (Exception e)
            {
                logger.Error("GetSalesSummary Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }

        }

        public OutputMessage InsertSalesSimple(SalesInputSimple salesInputSimple, String User, String Site, int Type)
        {
            User user = new User();
            logger.Debug("Start Connect");
            this.Connect();
            logger.Debug("End Connect");
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "PKKDSCMSSALES_INT.INS_DATA";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("PCMSSALNOTA", OracleDbType.Varchar2, 20).Value = "WEB";
                cmd.Parameters.Add("PCMSSALBRCD", OracleDbType.Varchar2, 20).Value = salesInputSimple.BARCODE;
                cmd.Parameters.Add("PCMSSALQTY", OracleDbType.Int32).Value = salesInputSimple.SALESQTY;
                cmd.Parameters.Add("PCMSSALSKU", OracleDbType.Int32).Value = salesInputSimple.DISCOUNT;
                cmd.Parameters.Add("PCMSSALFLAG", OracleDbType.Int32).Value = 1;
                cmd.Parameters.Add("PCMSSALSTAT", OracleDbType.Int32).Value = Type;
                cmd.Parameters.Add("PCMSSALCOMM", OracleDbType.Varchar2, 1000).Value = "";
                cmd.Parameters.Add("PCMSSALCDAT", OracleDbType.Date).Value = DateTime.Now;
                cmd.Parameters.Add("PCMSSALMDAT", OracleDbType.Date).Value = DateTime.Now;
                cmd.Parameters.Add("PCMSSALSITE", OracleDbType.Varchar2).Value = Site;
                cmd.Parameters.Add("PCMSSALCRBY", OracleDbType.Varchar2).Value = User;
                cmd.Parameters.Add("PCMSSALAMT", OracleDbType.Int32).Value = salesInputSimple.AMOUNT;
                cmd.Parameters.Add("PCMSSALTYPE", OracleDbType.Int32).Value = 1;
                cmd.Parameters.Add("PCMSSALTRNDATE", OracleDbType.Date).Value = salesInputSimple.TransDate;
                
                //Update GAGAN
                cmd.Parameters.Add("PCMSDISCOUNT", OracleDbType.Int32).Value = salesInputSimple.DISCOUNT;
                cmd.Parameters.Add("PCMSFINALPRICE", OracleDbType.Varchar2).Value = salesInputSimple.FinalPrice;
                cmd.Parameters.Add("PCMSNORMALPRICE", OracleDbType.Varchar2).Value = salesInputSimple.NormalPrice;

                cmd.Parameters.Add("POUTRSNCODE", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("POUTRSNMSG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;


                


                logger.Debug("Execute Command");
                logger.Debug(cmd.CommandText.ToString());

                cmd.ExecuteNonQuery();
                //OracleDataReader dr = cmd.ExecuteReader();
                logger.Debug("End Execute Command");
                outputMsg = new OutputMessage();

                outputMsg.Code = Int32.Parse(cmd.Parameters["POUTRSNCODE"].Value.ToString());
                outputMsg.Message = cmd.Parameters["POUTRSNMSG"].Value.ToString();

                logger.Debug("Start Close Connection");
                this.Close();
                logger.Debug("End Close Connection");
                return outputMsg;
            }
            catch (Exception e)
            {
                logger.Error("Login Function");
                logger.Error(e.Message);
                this.Close();
                return null;
            }
        }

        
    }




}
