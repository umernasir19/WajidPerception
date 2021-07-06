using SNDDAL;
using SSS.Property.Transactions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SSS.DAL.Transactions
{
    public class LP_Activity_DAL : DBInteractionBase
    {
        private LP_Activity_Property _objACTasterProperty;


        public LP_Activity_DAL()
        {

        }

        public LP_Activity_DAL(LP_Activity_Property objACTasterProperty)
        {
            _objACTasterProperty = objACTasterProperty;
        }

        public bool DeleteAndInsert()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "delete from Activity where orderIdx="+_objACTasterProperty.orderIdx;
            //cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;
            try
            {
                //cmdToExecute.Parameters.Add(new SqlParameter("@activityDate", SqlDbType.NVarChar, 50, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _objACTasterProperty.activityDate));
                //cmdToExecute.Parameters.Add(new SqlParameter("@creationDate", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _objACTasterProperty.creationDate));

                //cmdToExecute.Parameters.Add(new SqlParameter("@createdBy", SqlDbType.Int, 80, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _objACTasterProperty.createdBy));
                //cmdToExecute.Parameters.Add(new SqlParameter("@orderIdx", SqlDbType.Int, 50, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _objACTasterProperty.orderIdx));
                //cmdToExecute.Parameters.Add(new SqlParameter("@productIdx", SqlDbType.Int, 50, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _objACTasterProperty.productIdx));
                //cmdToExecute.Parameters.Add(new SqlParameter("@vendorCatIdx", SqlDbType.Int, 50, ParameterDirection.Input, true, 18, 1, "", DataRowVersion.Proposed, _objACTasterProperty.vendorCatIdx));

                //cmdToExecute.Parameters.Add(new SqlParameter("@vendorIdx", SqlDbType.Int, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _objACTasterProperty.vendorIdx));
                //cmdToExecute.Parameters.Add(new SqlParameter("@size", SqlDbType.NVarChar, 50, ParameterDirection.Input, true, 18, 1, "", DataRowVersion.Proposed, _objACTasterProperty.size));
                //cmdToExecute.Parameters.Add(new SqlParameter("@qty", SqlDbType.Decimal, 50, ParameterDirection.Input, true, 18, 1, "", DataRowVersion.Proposed, _objACTasterProperty.qty));

                //cmdToExecute.Parameters.Add(new SqlParameter("@activityPrice", SqlDbType.Decimal, 50, ParameterDirection.Input, true, 18, 1, "", DataRowVersion.Proposed, _objACTasterProperty.activityPrice));
                //cmdToExecute.Parameters.Add(new SqlParameter("@description", SqlDbType.NVarChar, 50, ParameterDirection.Input, true, 18, 1, "", DataRowVersion.Proposed, _objACTasterProperty.description));
                //cmdToExecute.Parameters.Add(new SqlParameter("@reference", SqlDbType.NVarChar, 50, ParameterDirection.Input, true, 18, 1, "", DataRowVersion.Proposed, _objACTasterProperty.reference));
                //cmdToExecute.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 32, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _objACTasterProperty.idx));
                //cmdToExecute.Parameters.Add(new SqlParameter("@typeIdx", SqlDbType.Int, 50, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _objACTasterProperty.typeIdx));
                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    //   _mainConnection.Open();
                    OpenConnection();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                this.StartTransaction();
                cmdToExecute.Transaction = this.Transaction;
                //// Execute query.
                _rowsAffected = cmdToExecute.ExecuteNonQuery();

                if (_objACTasterProperty.DetailData != null)
                {
                    decimal totalmount;
                    foreach (DataRow row in _objACTasterProperty.DetailData.Rows)
                    {
                        // row["doIdx"] = cmdToExecute.Parameters["@ID"].Value.ToString();
                        row["activityDate"] = _objACTasterProperty.activityDate;
                        row["creationDate"] = _objACTasterProperty.creationDate;
                        row["createdBy"] = _objACTasterProperty.createdBy;
                        row["orderIdx"] = _objACTasterProperty.orderIdx;
                        row["vendorCatIdx"] = _objACTasterProperty.vendorCatIdx;
                        row["vendorIdx"] = _objACTasterProperty.vendorIdx;
                        row["typeIdx"] = _objACTasterProperty.typeIdx;



                    }


                    _objACTasterProperty.DetailData.AcceptChanges();

                    SqlBulkCopy sbc = new SqlBulkCopy(_mainConnection, SqlBulkCopyOptions.Default, this.Transaction);
                    _objACTasterProperty.DetailData.TableName = "Activity";

                    sbc.ColumnMappings.Clear();
                    sbc.ColumnMappings.Add("activityDate", "activityDate");
                    sbc.ColumnMappings.Add("creationDate", "creationDate");
                    sbc.ColumnMappings.Add("createdBy", "createdBy");
                    sbc.ColumnMappings.Add("orderIdx", "orderIdx");
                    sbc.ColumnMappings.Add("vendorCatIdx", "vendorCatIdx");
                    sbc.ColumnMappings.Add("vendorIdx", "vendorIdx");
                    sbc.ColumnMappings.Add("typeIdx", "typeIdx");
                    sbc.ColumnMappings.Add("productIdx", "productIdx");
                    sbc.ColumnMappings.Add("size", "size");
                    sbc.ColumnMappings.Add("qty", "qty");
                    sbc.ColumnMappings.Add("activityPrice", "activityPrice");
                    sbc.ColumnMappings.Add("description", "description");
                    sbc.ColumnMappings.Add("reference", "reference");
                    sbc.ColumnMappings.Add("totalAmount", "totalAmount");
                    sbc.DestinationTableName = _objACTasterProperty.DetailData.TableName;
                    sbc.WriteToServer(_objACTasterProperty.DetailData);

                }
                this.Commit();
                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    this.RollBack();
                    throw new Exception("Stored Procedure 'sp_TRANSACTION_MASTER_Insert' reported the ErrorCode: " + _errorCode);

                }

                return true;
            }
            catch (Exception ex)
            {
                this.RollBack();
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("TRANSACTION_MASTER::Insert::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    //// Close connection.
                    //_mainConnection.Close();
                    CloseConnection();
                }
                cmdToExecute.Dispose();
            }
        }



        public override bool Insert()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            //cmdToExecute.CommandText = "dbo.[sp_localPurchase_Insert]";
            //cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;
            try
            {
                //cmdToExecute.Parameters.Add(new SqlParameter("@activityDate", SqlDbType.NVarChar, 50, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _objACTasterProperty.activityDate));
                //cmdToExecute.Parameters.Add(new SqlParameter("@creationDate", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _objACTasterProperty.creationDate));

                //cmdToExecute.Parameters.Add(new SqlParameter("@createdBy", SqlDbType.Int, 80, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _objACTasterProperty.createdBy));
                //cmdToExecute.Parameters.Add(new SqlParameter("@orderIdx", SqlDbType.Int, 50, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _objACTasterProperty.orderIdx));
                //cmdToExecute.Parameters.Add(new SqlParameter("@productIdx", SqlDbType.Int, 50, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _objACTasterProperty.productIdx));
                //cmdToExecute.Parameters.Add(new SqlParameter("@vendorCatIdx", SqlDbType.Int, 50, ParameterDirection.Input, true, 18, 1, "", DataRowVersion.Proposed, _objACTasterProperty.vendorCatIdx));

                //cmdToExecute.Parameters.Add(new SqlParameter("@vendorIdx", SqlDbType.Int, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _objACTasterProperty.vendorIdx));
                //cmdToExecute.Parameters.Add(new SqlParameter("@size", SqlDbType.NVarChar, 50, ParameterDirection.Input, true, 18, 1, "", DataRowVersion.Proposed, _objACTasterProperty.size));
                //cmdToExecute.Parameters.Add(new SqlParameter("@qty", SqlDbType.Decimal, 50, ParameterDirection.Input, true, 18, 1, "", DataRowVersion.Proposed, _objACTasterProperty.qty));
                
                //cmdToExecute.Parameters.Add(new SqlParameter("@activityPrice", SqlDbType.Decimal, 50, ParameterDirection.Input, true, 18, 1, "", DataRowVersion.Proposed, _objACTasterProperty.activityPrice));
                //cmdToExecute.Parameters.Add(new SqlParameter("@description", SqlDbType.NVarChar, 50, ParameterDirection.Input, true, 18, 1, "", DataRowVersion.Proposed, _objACTasterProperty.description));
                //cmdToExecute.Parameters.Add(new SqlParameter("@reference", SqlDbType.NVarChar, 50, ParameterDirection.Input, true, 18, 1, "", DataRowVersion.Proposed, _objACTasterProperty.reference));
                //cmdToExecute.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 32, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _objACTasterProperty.idx));
                //cmdToExecute.Parameters.Add(new SqlParameter("@typeIdx", SqlDbType.Int, 50, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _objACTasterProperty.typeIdx));
                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    //   _mainConnection.Open();
                    OpenConnection();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                this.StartTransaction();
                //cmdToExecute.Transaction = this.Transaction;
                //// Execute query.
                //_rowsAffected = cmdToExecute.ExecuteNonQuery();

                if (_objACTasterProperty.DetailData != null)
                {
                    decimal totalmount;
                    foreach (DataRow row in _objACTasterProperty.DetailData.Rows)
                    {
                       // row["doIdx"] = cmdToExecute.Parameters["@ID"].Value.ToString();
                        row["activityDate"] = _objACTasterProperty.activityDate;
                        row["creationDate"] = _objACTasterProperty.creationDate;
                        row["createdBy"] = _objACTasterProperty.createdBy;
                        row["orderIdx"] = _objACTasterProperty.orderIdx;
                        //row["vendorCatIdx"] = _objACTasterProperty.vendorCatIdx;
                        //row["vendorIdx"] = _objACTasterProperty.vendorIdx;
                        row["typeIdx"] = _objACTasterProperty.typeIdx;
                        row["DeliveryDate"] = _objACTasterProperty.DeliveryDate;



                    }


                    _objACTasterProperty.DetailData.AcceptChanges();

                    SqlBulkCopy sbc = new SqlBulkCopy(_mainConnection, SqlBulkCopyOptions.Default, this.Transaction);
                    _objACTasterProperty.DetailData.TableName = "Activity";

                    sbc.ColumnMappings.Clear();
                    sbc.ColumnMappings.Add("activityDate", "activityDate");
                    sbc.ColumnMappings.Add("creationDate", "creationDate");
                    sbc.ColumnMappings.Add("DeliveryDate", "DeliveryDate");
                    sbc.ColumnMappings.Add("createdBy", "createdBy");
                    sbc.ColumnMappings.Add("orderIdx", "orderIdx");
                    sbc.ColumnMappings.Add("vendorCatIdx", "vendorCatIdx");
                    sbc.ColumnMappings.Add("vendorIdx", "vendorIdx");
                    sbc.ColumnMappings.Add("typeIdx", "typeIdx");
                    sbc.ColumnMappings.Add("productIdx", "productIdx");
                    sbc.ColumnMappings.Add("size", "size");
                    sbc.ColumnMappings.Add("qty", "qty");
                    sbc.ColumnMappings.Add("activityPrice", "activityPrice");
                    sbc.ColumnMappings.Add("description", "description");
                    sbc.ColumnMappings.Add("reference", "reference");
                    sbc.ColumnMappings.Add("totalAmount", "totalAmount");
                    sbc.DestinationTableName = _objACTasterProperty.DetailData.TableName;
                    sbc.WriteToServer(_objACTasterProperty.DetailData);

                }
                this.Commit();
                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    this.RollBack();
                    throw new Exception("Stored Procedure 'sp_TRANSACTION_MASTER_Insert' reported the ErrorCode: " + _errorCode);

                }

                return true;
            }
            catch (Exception ex)
            {
                this.RollBack();
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("TRANSACTION_MASTER::Insert::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    //// Close connection.
                    //_mainConnection.Close();
                    CloseConnection();
                }
                cmdToExecute.Dispose();
            }
        }
        public  DataTable selectVendorPrice(int id)
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = @"select isNULL(price,0) price from vendors where idx=@idx";
            
            DataTable toReturn = new DataTable("Vendor Setup");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@idx", SqlDbType.Int,500, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, id));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                adapter.Fill(toReturn);


     
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("PRODUCT_SETUP::SelectOne::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public override DataTable SelectAll()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "sp_GetAllActivity";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("Vendor Setup");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 500, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _objACTasterProperty.orderIdx));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                adapter.Fill(toReturn);



                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("PRODUCT_SETUP::SelectOne::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

    }
}
