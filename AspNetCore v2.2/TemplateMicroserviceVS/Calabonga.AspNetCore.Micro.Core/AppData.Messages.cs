namespace $safeprojectname$
{
    public static partial class AppData
    {
        /// <summary>
        /// Common messages
        /// </summary>
        public static class Messages
        {
            /// <summary>
            /// "User successfully registered"
            /// </summary>
            public static string UserSuccessfullyRegistered => "User successfully registered";

            /// <summary>
            /// "Hotel successfully saved"
            /// </summary>
            public static string PropertySuccessfullySaved => "Hotel successfully saved";

            /// <summary>
            /// "Access Denied"
            /// </summary>
            public static string AccessDenied => "Access Denied";

            /// <summary>
            /// "ViewModel {0} for template generation not registered";
            /// </summary>
            public static string ViewModelForTemplateNotRegistered => "ViewModel {0} for template generation not registered";

            /// <summary>
            /// "Email already registered"
            /// </summary>
            public static string EmailAlreadyRegistered => "Email already registered";

            #region Import

            /// <summary>
            /// "Import reservations started"
            /// </summary>
            public static string ImportReservationsBegin => "Import reservations started";

            /// <summary>
            /// "Import RoomTypes started"
            /// </summary>
            public static string ImportRoomTypesBegin => "Import RoomTypes started";

            /// <summary>
            /// "Import reservations from Excel-file contains {0} columns and {1} rows"
            /// </summary>
            public static string ImportReservationsExcelInfo => "Import reservations from Excel-file contains {0} columns and {1} rows";

            /// <summary>
            /// "Import reservation begin parsing"
            /// </summary>
            public static string ImportReservationsBeginParsing => "Import reservation begin parsing";

            /// <summary>
            /// "ImportRoomTypes begin parsing"
            /// </summary>
            public static string ImportRoomTypesBeginParsing => "ImportRoomTypes begin parsing";

            /// <summary>
            /// "Import reservation Errors founded: {0}"
            /// </summary>
            public static string ImportReservationsErrorsFound => "Import reservation Errors found: {0}";

            /// <summary>
            /// "ImportRoomTypes Errors found: {0}"
            /// </summary>
            public static string ImportRoomTypesErrorsFound => "ImportRoomTypes Errors found: {0}";

            /// <summary>
            /// "Import reservation items prepared: {0}"
            /// </summary>
            public static string ImportReservationItemsPrepared => "Import reservation items prepared: {0}";

            /// <summary>
            /// "Import reservation successfully competed. Imported: {0} (Reservation Import Strategy: {1})"
            /// </summary>
            public static string ImportReservationsComplete => "Import reservation successfully competed. Imported: {0} (Reservation Import Strategy: {1})";

            /// <summary>
            /// "Import RoomType successfully competed. Imported: {0}"
            /// </summary>
            public static string ImportRoomTypeComplete => "Import RoomType successfully competed. Imported: {0}";

            /// <summary>
            /// "Import reservation trying to get Data from file"
            /// </summary>
            public static string ImportReservationsTryToGetData => "Import reservation trying to get Data from file";

            /// <summary>
            /// "Import RoomTypes trying to get Data from file"
            /// </summary>
            public static string ImportRoomTypesTryToGetData => "Import RoomTypes trying to get Data from file";

            /// <summary>
            /// "Import reservation extracting data from {0}-file"
            /// </summary>
            public static string ImportReservationsFormatFound => "Import reservation extracting data from {0}-file";

            /// <summary>
            /// "Import reservations successfully completed. Imported: {0}"
            /// </summary>
            public static string ImportReservationsCompleted => "Import reservations successfully completed. Imported: {0}";

            /// <summary>
            /// "Import reservations now saving data to file {0}"
            /// </summary>
            public static string ImportReservationSaveImportedToFile => "Import reservations now saving data to file {0}";

            /// <summary>
            /// "Import reservations now queued to processing"
            /// </summary>
            public static string ImportReservationEmailBeforeStartProcessing => "Import reservations now queued to processing";

            #endregion

            /// <summary>
            /// "Entity successfully deleted"
            /// </summary>
            public static string EntitySuccessfullyDeleted => "Entity successfully deleted";

            /// <summary>
            /// "Manually uploaded"
            /// </summary>
            public static string PricepointsManuallyUploadedText => "Manually uploaded";

            /// <summary>
            /// "Automatically generated by {0}"
            /// </summary>
            public static string ViewModelFactoryGenerationText => "Automatically generated by {0}";

            /// <summary>
            /// "RoomType maps not found for this type of room types";
            /// </summary>
            public static string RoomTypeMapsNotFound => "RoomType maps not found for this type of room types";

            /// <summary>
            /// "Push notification {0} disabled in the configuration";
            /// </summary>
            public static string PushNotificationDisabled => "Push notification disabled by configuration";

            /// <summary>
            /// User has not been registered any devices
            /// </summary>
            public static string UserDevicesNotFound => "User has not been registered any devices";

            /// <summary>
            /// "Total uUser mobile devices found"
            /// </summary>
            public static string UserMobileDevicesFound => "Total user mobile devices found";

            #region Scheduler

            /// <summary>
            /// "[SCHEDULER]: Scheduler started"
            /// </summary>
            public static string SchedulerStarted => "[SCHEDULER]: Scheduler started";

            /// <summary>
            ///  "[SCHEDULER]: Scheduler work executing time successfully updated"
            /// </summary>
            public static string SchedulerWorkUpdated => "[SCHEDULER]: Scheduler work executing time successfully updated";
            
            #endregion

            /// <summary>
            /// KPI rule fired
            /// </summary>
            public static string KpiRuleFired => "KPI rule fired";

            /// <summary>
            /// "Password reset requested for email {0}. UserId {1}  Code: {2}"
            /// </summary>
            public static string PasswordResetRequested => "Password reset requested for email {0}. UserId {1}  Code: {2}";

            /// <summary>
            /// "Password successfully changed"
            /// </summary>
            public static string PasswordSuccessfullyReset => "Password successfully changed";

            #region Recommendations

            /// <summary>
            /// "No recommendation found for room type [{0}] in hotel [{1}] between [{2}] and [{3}]"
            /// </summary>
            public static string RecommendationNotFound => "No recommendation found for room type [{0}] in hotel [{1}] between [{2}] and [{3}]";

            /// <summary>
            /// "Price recommendation received"
            /// </summary>
            public static string PriceRecommendationReceived => "Price recommendation received";

            #endregion

            #region Strategies

            /// <summary>
            /// "[STRATEGY]: Strategy successfully applied"
            /// </summary>
            public static string StrategySuccessfullyApplied => "[STRATEGY]: Strategy successfully applied";

            /// <summary>
            /// "[STRATEGY]: Room types not provided"
            /// </summary>
            public static string RoomTypesNotProvided => "[STRATEGY]: Room types not provided";

            /// <summary>
            /// "[STRATEGY]: Strategies not found for hotel {0}"
            /// </summary>
            public static string StrategiesNotFoundedForHotel => "[STRATEGY]: Strategies not found for hotel {0}";

            /// <summary>
            /// "[STRATEGY]: Strategy calculation for room type {0} started with strategy {1}";
            /// </summary>
            public static string StrategyCalculationForRoomTypeStarted => "[STRATEGY]: Strategy calculation for room type {0} started with strategy {1}";

            /// <summary>
            /// "[STRATEGY]: Strategy calculation skipped because there are no pricepoints processed";
            /// </summary>
            public static string StrategyCalculationSkipped => "[STRATEGY]: Strategy calculation skipped because there are no pricepoints processed";

            #endregion

            /// <summary>
            /// "Budgets not found for hotel"
            /// </summary>
            public static string BudgetNotFound => "Budgets not found for hotel";

            /// <summary>
            /// "No pricepoints found for change status operation"
            /// </summary>
            public static string PricepointsNotFoundForStatusChangeOperation => "No pricepoints found for change status operation";

            /// <summary>
            /// "Find competitors work created"
            /// </summary>
            public static string FindCompetitorWorkCreated => "Find competitors work created";

            /// <summary>
            /// "RoomTypes found for Pricepoint modifications for strategies";
            /// </summary>
            public static string RoomTypesFoundedForStrategyModificator => "RoomTypes found for Pricepoint modifications for strategies";
            
            #region Exschange

            /// <summary>
            /// "[EXCHANGE]: Pricepoint for exchange operation work successfully created";
            /// </summary>
            public static string PricepointForExchangeWorkCreated => "[EXCHANGE]: Pricepoint for exchange operation work successfully created";

            /// <summary>
            /// "[EXCHANGE]: Pricepoint for exchange operation work successfully updated";
            /// </summary>
            public static string PricepointForExchangeWorkUpdated => "[EXCHANGE]: Pricepoint for exchange operation work successfully updated";

            #endregion

            #region Work

            /// <summary>
            /// "[WORK]: Nothing to do at {0}"
            /// </summary>
            public static string NothingToDo => "[WORK]: Nothing to do at {0}";

            /// <summary>
            /// "[WORK]: Totally founded for processing {0}"
            /// </summary>
            public static string TotalWorkToProcessFound => "[WORK]: Totally founded for processing {0}";

            #endregion
        }
    }
}
