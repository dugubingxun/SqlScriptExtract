using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SqlScriptExtract
{
    public class ClsDataADODB
    {
        public Boolean  flag_del; //确认删除标志
        public Boolean  flag_save; //保存成功标志
        public Boolean  booledit;
        public object  mvbookmark;
        //public Boolean Add(ref ADODB.Recordset adoPrimaryRS,object mvbookmark)
        //{
        //     try
        //     {
        //         if(adoPrimaryRS.RecordCount>0 && !(adoPrimaryRS.EOF || adoPrimaryRS.BOF  ))
        //         {
        //             mvbookmark=adoPrimaryRS.Bookmark ;
        //         }
        //         else
        //         {
        //             mvbookmark = 0;
        //         }
        //         adoPrimaryRS.AddNew();
        //         return true ;
        //     }
        //     catch (Exception ex)
        //     {
        //         adoPrimaryRS.CancelBatch(ADODB.AffectEnum.adAffectCurrent );
        //         adoPrimaryRS.Bookmark = mvbookmark;
        //         return false ;
        //     }
        //}

        //public Boolean edit(ref ADODB.Recordset adoPrimaryRS, object mvbookmark)
        //{
        //    try
        //    {
        //        if (adoPrimaryRS.RecordCount > 0)
        //        {
        //            mvbookmark = adoPrimaryRS.Bookmark;
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        adoPrimaryRS.Bookmark = mvbookmark;
        //        return false;
        //    }
        //}
        //public Boolean Update(ref ADODB.Recordset adoPrimaryRS, object mvbookmark, Boolean editmode)
        //{
        //    try
        //    {
        //        flag_save =false ;
        //        adoPrimaryRS.UpdateBatch(ADODB.AffectEnum.adAffectCurrent); //adAffectCurrent
        //        flag_save =true;
        //        if( editmode==false) 
        //        {
        //            //adoPrimaryRS.Requery();
        //            adoPrimaryRS.MoveLast();
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        adoPrimaryRS.CancelBatch(ADODB.AffectEnum.adAffectCurrent);
        //        flag_save = false;
        //        if( (int)mvbookmark != 0)
        //        {
        //          adoPrimaryRS.Bookmark = mvbookmark;
        //        }
        //        return false;
        //    }
        //}
        //public Boolean Updatecurrent(ref ADODB.Recordset adoPrimaryRS, object mvbookmark, Boolean editmode)
        //{
        //    try
        //    {
        //        flag_save = false;
        //        adoPrimaryRS.UpdateBatch(ADODB.AffectEnum.adAffectAllChapters); //adAffectCurrent
        //        flag_save = true;
        //        if (editmode == false)
        //        {
        //            adoPrimaryRS.Requery();
        //            adoPrimaryRS.MoveLast();
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        adoPrimaryRS.CancelBatch(ADODB.AffectEnum.adAffectAllChapters);
        //        return false ;
        //    }
        //}
        //public Boolean Cancel(ref ADODB.Recordset adoPrimaryRS, object mvbookmark, Boolean flag)
        //{
        //    try
        //    {
        //        adoPrimaryRS.CancelUpdate();
        //        if (adoPrimaryRS.RecordCount <= 0)
        //        {
        //            return true;
        //        }
        //        if(! flag && (int)mvbookmark != 0)
        //        {
        //            adoPrimaryRS.Bookmark = mvbookmark;
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        //public Boolean MoveNext(ref ADODB.Recordset adoPrimaryRS)
        //{
        //    try
        //    {
        //        if (adoPrimaryRS.State == 0)
        //        {
        //            return false;
        //        }
        //        if (adoPrimaryRS.RecordCount <= 0)
        //        {
                    
        //            return false;
        //        }
        //        if (!adoPrimaryRS.EOF)
        //        {
        //            adoPrimaryRS.MoveNext();
        //        }
        //        if( adoPrimaryRS.EOF && adoPrimaryRS.RecordCount > 0) 
        //        {
        //            adoPrimaryRS.MoveLast();
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        //public Boolean moveprev(ref ADODB.Recordset adoPrimaryRS)
        //{
        //    try
        //    {
        //        if (adoPrimaryRS.State == 0)
        //        {
        //            return false;
        //        }
        //        if (adoPrimaryRS.RecordCount <= 0)
        //        {

        //            return false;
        //        }
        //        if (!adoPrimaryRS.BOF)
        //        {
        //            adoPrimaryRS.MovePrevious ();
        //        }
        //        if (adoPrimaryRS.BOF  && adoPrimaryRS.RecordCount > 0)
        //        {
        //            adoPrimaryRS.MoveFirst ();
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        //public Boolean MoveFirst(ref ADODB.Recordset adoPrimaryRS)
        //{
        //    try
        //    {
        //        if (adoPrimaryRS.State == 0)
        //        {
        //            return false;
        //        }
        //        if ( adoPrimaryRS.RecordCount > 0)
        //        {
        //            adoPrimaryRS.MoveFirst();
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        //public Boolean MoveLast(ref ADODB.Recordset adoPrimaryRS)
        //{
        //    try
        //    {
        //        if (adoPrimaryRS.State == 0)
        //        {
        //            return false;
        //        }
        //        if (adoPrimaryRS.RecordCount > 0)
        //        {
        //            adoPrimaryRS.MoveLast ();
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        //public Boolean Delete(ref ADODB.Recordset adoPrimaryRS, object mvbookmark)
        //{
        //    try
        //    {
        //        if (adoPrimaryRS.RecordCount <= 0)
        //        {

        //            return false;
        //        }
        //         mvbookmark =adoPrimaryRS.Bookmark ;
        //         adoPrimaryRS.Delete(ADODB.AffectEnum.adAffectCurrent);
        //         if (MessageBox.Show("确认删除吗", "是否确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
        //         {
        //             adoPrimaryRS.UpdateBatch(ADODB.AffectEnum.adAffectCurrent);
        //             flag_del = true;
        //             if (adoPrimaryRS.RecordCount <= 0)
        //             {
        //                 adoPrimaryRS.Requery();
        //             }
        //             adoPrimaryRS.MoveNext();
        //             if (adoPrimaryRS.EOF)
        //             {
        //                 adoPrimaryRS.MoveLast();
        //             }
        //         }
        //         else
        //         {
        //             adoPrimaryRS.CancelUpdate();
        //             flag_del = false;
        //             adoPrimaryRS.Bookmark = mvbookmark;
        //         }


        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        adoPrimaryRS.CancelBatch(ADODB.AffectEnum.adAffectCurrent);
        //        if ((int)mvbookmark != 0)
        //        {
        //            adoPrimaryRS.Bookmark = mvbookmark;
        //        }
        //        return false;
        //    }
        //}
        //public Boolean Deletedj(ref ADODB.Recordset adoPrimaryRS, object mvbookmark)
        //{
        //    try
        //    {
        //        if (adoPrimaryRS.RecordCount <= 0)
        //        {

        //            return false;
        //        }
        //        mvbookmark = adoPrimaryRS.Bookmark;
        //        adoPrimaryRS.Delete(ADODB.AffectEnum.adAffectCurrent);
        //        adoPrimaryRS.UpdateBatch(ADODB.AffectEnum.adAffectCurrent);
        //        flag_del = true;
        //        if (adoPrimaryRS.RecordCount <= 0)
        //        {
        //            adoPrimaryRS.Requery();
        //        }
        //        adoPrimaryRS.MoveNext();
        //        if (adoPrimaryRS.EOF)
        //        {
        //            adoPrimaryRS.MoveLast();
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
       
    }
}
