using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Consts
{
    public class ErrorCodesConsts
    {
        public const string INVALID_REQUEST = "InvalidRequest";
        public const string ERROR_OCCURED = "ErrorOccured";
        public const string NOT_FOUND = "NotFound";
        public const string INVALID_SECRET = "InvalidSecret";
        public const string INVALID_SECRET_SERIAL = "InvalidSecretSerial";
        public const string SUCCESS = "Success";
        public const string INVALID_VERSION = "InvalidVersion";
        public const string INVALID_SERIAL = "InvalidSerial";
        public const string INVALID_DEVICE_ID = "InvalidDeviceId";
        public const string READINGS_EMPTY= "ReadingsEmpty";
        public const string NOT_ALL_READINGS_SAVED = "NotAllReadingsSaved";
        public const string INVALID_MESSAGE = "InvalidMessage";
        public const string INVALID_CODE = "InvalidCode";
        public const string INVALID_ALERT_DATE = "InvalidAlertDate";
        public const string INVALID_SENSOR_READING = "InvalidSensorReading";
        public const string INVALID_USERNAME = "InvalidUserName";
        public const string INVALID_PASSWORD = "InvalidPassword";
        public const string INVALID_USERNAME_PASSWORD = "InvalidUserNameOrPassword";
        public const string ALREADY_LOGGED_OUT = "AlreadyLoggedOut";
    }
}
