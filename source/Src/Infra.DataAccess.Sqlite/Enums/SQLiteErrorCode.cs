namespace DotFramework.Infra.DataAccess.Sqlite
{
    //
    // Summary:
    //     SQLite error codes. Actually, this enumeration represents a return code, which
    //     may also indicate success in one of several ways (e.g. SQLITE_OK, SQLITE_ROW,
    //     and SQLITE_DONE). Therefore, the name of this enumeration is something of a misnomer.
    public enum SQLiteErrorCode
    {
        //
        // Summary:
        //     The error code is unknown. This error code is only used by the managed wrapper
        //     itself.
        Unknown = -1,
        //
        // Summary:
        //     Successful result
        Ok = 0,
        //
        // Summary:
        //     SQL error or missing database
        Error = 1,
        //
        // Summary:
        //     Internal logic error in SQLite
        Internal = 2,
        //
        // Summary:
        //     Access permission denied
        Perm = 3,
        //
        // Summary:
        //     Callback routine requested an abort
        Abort = 4,
        //
        // Summary:
        //     The database file is locked
        Busy = 5,
        //
        // Summary:
        //     A table in the database is locked
        Locked = 6,
        //
        // Summary:
        //     A malloc() failed
        NoMem = 7,
        //
        // Summary:
        //     Attempt to write a readonly database
        ReadOnly = 8,
        //
        // Summary:
        //     Operation terminated by sqlite3_interrupt()
        Interrupt = 9,
        //
        // Summary:
        //     Some kind of disk I/O error occurred
        IoErr = 10,
        //
        // Summary:
        //     The database disk image is malformed
        Corrupt = 11,
        //
        // Summary:
        //     Unknown opcode in sqlite3_file_control()
        NotFound = 12,
        //
        // Summary:
        //     Insertion failed because database is full
        Full = 13,
        //
        // Summary:
        //     Unable to open the database file
        CantOpen = 14,
        //
        // Summary:
        //     Database lock protocol error
        Protocol = 15,
        //
        // Summary:
        //     Database is empty
        Empty = 16,
        //
        // Summary:
        //     The database schema changed
        Schema = 17,
        //
        // Summary:
        //     String or BLOB exceeds size limit
        TooBig = 18,
        //
        // Summary:
        //     Abort due to constraint violation
        Constraint = 19,
        //
        // Summary:
        //     Data type mismatch
        Mismatch = 20,
        //
        // Summary:
        //     Library used incorrectly
        Misuse = 21,
        //
        // Summary:
        //     Uses OS features not supported on host
        NoLfs = 22,
        //
        // Summary:
        //     Authorization denied
        Auth = 23,
        //
        // Summary:
        //     Auxiliary database format error
        Format = 24,
        //
        // Summary:
        //     2nd parameter to sqlite3_bind out of range
        Range = 25,
        //
        // Summary:
        //     File opened that is not a database file
        NotADb = 26,
        //
        // Summary:
        //     Notifications from sqlite3_log()
        Notice = 27,
        //
        // Summary:
        //     Warnings from sqlite3_log()
        Warning = 28,
        //
        // Summary:
        //     sqlite3_step() has another row ready
        Row = 100,
        //
        // Summary:
        //     sqlite3_step() has finished executing
        Done = 101,
        //
        // Summary:
        //     Used to mask off extended result codes
        NonExtendedMask = 255,
        //
        // Summary:
        //     Success. Prevents the extension from unloading until the process terminates.
        Ok_Load_Permanently = 256,
        //
        // Summary:
        //     A database file is locked due to a recovery operation.
        Busy_Recovery = 261,
        //
        // Summary:
        //     A database table is locked in shared-cache mode.
        Locked_SharedCache = 262,
        //
        // Summary:
        //     A database file is read-only due to a recovery operation.
        ReadOnly_Recovery = 264,
        //
        // Summary:
        //     A file read operation failed.
        IoErr_Read = 266,
        //
        // Summary:
        //     A virtual table is malformed.
        Corrupt_Vtab = 267,
        //
        // Summary:
        //     A database file cannot be opened because no temporary directory is available.
        CantOpen_NoTempDir = 270,
        //
        // Summary:
        //     A CHECK constraint failed.
        Constraint_Check = 275,
        //
        // Summary:
        //     User authentication failed.
        Auth_User = 279,
        //
        // Summary:
        //     Frames were recovered from the WAL log file.
        Notice_Recover_Wal = 283,
        //
        // Summary:
        //     An automatic index was created to process a query.
        Warning_AutoIndex = 284,
        //
        // Summary:
        //     An operation is being aborted due to rollback processing.
        Abort_Rollback = 516,
        //
        // Summary:
        //     A database file is locked due to snapshot semantics.
        Busy_Snapshot = 517,
        //
        // Summary:
        //     A database file is read-only because a lock could not be obtained.
        ReadOnly_CantLock = 520,
        //
        // Summary:
        //     A file read operation returned less data than requested.
        IoErr_Short_Read = 522,
        //
        // Summary:
        //     A database file cannot be opened because its path represents a directory.
        CantOpen_IsDir = 526,
        //
        // Summary:
        //     A commit hook produced a unsuccessful return code.
        Constraint_CommitHook = 531,
        //
        // Summary:
        //     Pages were recovered from the journal file.
        Notice_Recover_Rollback = 539,
        //
        // Summary:
        //     A database file is read-only because it needs rollback processing.
        ReadOnly_Rollback = 776,
        //
        // Summary:
        //     A file write operation failed.
        IoErr_Write = 778,
        //
        // Summary:
        //     A database file cannot be opened because its full path could not be obtained.
        CantOpen_FullPath = 782,
        //
        // Summary:
        //     A FOREIGN KEY constraint failed.
        Constraint_ForeignKey = 787,
        //
        // Summary:
        //     A database file is read-only because it was moved while open.
        ReadOnly_DbMoved = 1032,
        //
        // Summary:
        //     A file synchronization operation failed.
        IoErr_Fsync = 1034,
        //
        // Summary:
        //     A database file cannot be opened because a path string conversion operation failed.
        CantOpen_ConvPath = 1038,
        //
        // Summary:
        //     Not currently used.
        Constraint_Function = 1043,
        //
        // Summary:
        //     A directory synchronization operation failed.
        IoErr_Dir_Fsync = 1290,
        //
        // Summary:
        //     A NOT NULL constraint failed.
        Constraint_NotNull = 1299,
        //
        // Summary:
        //     A file truncate operation failed.
        IoErr_Truncate = 1546,
        //
        // Summary:
        //     A PRIMARY KEY constraint failed.
        Constraint_PrimaryKey = 1555,
        //
        // Summary:
        //     A file metadata operation failed.
        IoErr_Fstat = 1802,
        //
        // Summary:
        //     The RAISE function was used by a trigger-program.
        Constraint_Trigger = 1811,
        //
        // Summary:
        //     A file unlock operation failed.
        IoErr_Unlock = 2058,
        //
        // Summary:
        //     A UNIQUE constraint failed.
        Constraint_Unique = 2067,
        //
        // Summary:
        //     A file lock operation failed.
        IoErr_RdLock = 2314,
        //
        // Summary:
        //     Not currently used.
        Constraint_Vtab = 2323,
        //
        // Summary:
        //     A file delete operation failed.
        IoErr_Delete = 2570,
        //
        // Summary:
        //     A ROWID constraint failed.
        Constraint_RowId = 2579,
        //
        // Summary:
        //     Not currently used.
        IoErr_Blocked = 2826,
        //
        // Summary:
        //     Out-of-memory during a file operation.
        IoErr_NoMem = 3082,
        //
        // Summary:
        //     A file existence/status operation failed.
        IoErr_Access = 3338,
        //
        // Summary:
        //     A check for a reserved lock failed.
        IoErr_CheckReservedLock = 3594,
        //
        // Summary:
        //     A file lock operation failed.
        IoErr_Lock = 3850,
        //
        // Summary:
        //     A file close operation failed.
        IoErr_Close = 4106,
        //
        // Summary:
        //     A directory close operation failed.
        IoErr_Dir_Close = 4362,
        //
        // Summary:
        //     A shared memory open operation failed.
        IoErr_ShmOpen = 4618,
        //
        // Summary:
        //     A shared memory size operation failed.
        IoErr_ShmSize = 4874,
        //
        // Summary:
        //     A shared memory lock operation failed.
        IoErr_ShmLock = 5130,
        //
        // Summary:
        //     A shared memory map operation failed.
        IoErr_ShmMap = 5386,
        //
        // Summary:
        //     A file seek operation failed.
        IoErr_Seek = 5642,
        //
        // Summary:
        //     A file delete operation failed because it does not exist.
        IoErr_Delete_NoEnt = 5898,
        //
        // Summary:
        //     A file memory mapping operation failed.
        IoErr_Mmap = 6154,
        //
        // Summary:
        //     The temporary directory path could not be obtained.
        IoErr_GetTempPath = 6410,
        //
        // Summary:
        //     A path string conversion operation failed.
        IoErr_ConvPath = 6666,
        //
        // Summary:
        //     Reserved.
        IoErr_VNode = 6922,
        //
        // Summary:
        //     An attempt to authenticate failed.
        IoErr_Auth = 7178
    }
}
