wxSQLite3 component info
------------------------

Website:      http://wxcode.sourceforge.net/components/wxsqlite3
Version:      1.9.8
Description:
wxSQLite3 is a C++ wrapper around the public domain SQLite 3.x database
and is specifically designed for use in programs based on the wxWidgets
library.

wxSQLite3 does not try to hide the underlying database, in contrary
almost all special features of the current SQLite3 version 3.6.22 are
supported, like for example the creation of user defined scalar or
aggregate functions.

Since SQLite stores strings in UTF-8 encoding, the wxSQLite3 methods
provide automatic conversion between wxStrings and UTF-8 strings. This
works best for the Unicode builds of wxWidgets. In ANSI builds the
current locale conversion object (wxConvCurrent) is used for conversion
to/from UTF-8. Special care has to be taken if external administration
tools are used to modify the database contents, since not all of these
tools operate in Unicode or UTF-8 mode.


Version history
---------------

 1.9.8 - Upgrade to SQLite version 3.6.22
         Fixed a bug when compiling without precompiled header support
         (by including wx/arrstr.h)
         Added experimental support for 256 bit AES encryption to the
         optional key based encryption extension
 1.9.7 - Upgrade to SQLite version 3.6.20
         Added methods to query, enable or disable foreign key support
 1.9.6 - Upgrade to SQLite version 3.6.18
         Added method to get the SQLite library source id
         Added flags parameter to wxSQLite3Database::Open to allow
         additional control over the database connection
         (see http://www.sqlite.org/c3ref/open.html for further information)
         Fixed a potential memory leak in wxSQLite3Statement class
         Converted encryption extension from C++ to pure C to make it
         compatible with the SQLite amalgamation.
 1.9.5 - Upgrade to SQLite version 3.6.11
         Added user defined function class for REGEXP operator
         Added support for SQLite backup/restore API, introduced with SQLite 3.6.11
 1.9.4 - Upgrade to SQLite version 3.6.10
         Added support for SQLite savepoints, introduced with SQLite 3.6.8
         Added IsOk methods to several classes
 1.9.3 - Upgrade to SQLite version 3.6.7
         Fixed a bug in method wxSQLite3Table::GetDouble
         (conversion from string to double failed in non-US locales)
         Build system upgraded using Bakefile 0.2.5
 1.9.2 - Upgrade to SQLite version 3.6.6
         Added RAII transaction class (see docs for details)
 1.9.1 - Upgrade to SQLite version 3.6.2
         Introduced own step counting for aggregate user functions
         since the sqlite3_aggregate_count function is now deprecated.
         Enhanced wxSQLite3Database::TableExists method to query an attached
         database for existence of a table or to query all open databases.
 1.9.0 - Upgrade to SQLite version 3.6.0
         The optional key based encryption support has been adapted to
         support SQLite version 3.6.0.
         Added static methods to initialize and shutdown the SQLite library.
         Changed build system to support static library build against shared
         wxWidgets build on Linux.
         Changed behaviour of Close method of class wxSQLite3Database to
         finalize all unfinalized prepared statements.
 1.8.5 - Upgrade to SQLite version 3.5.9
         Integration of the optional key based encryption support into SQLite
         has been made easier. Changes to original SQLite source files
         are no longer necessary.
 1.8.4 - Upgrade to SQLite version 3.5.8
         Added support for accessing database limits
         Changed method TableExists to check a table name case insensitive
         Fixed several minor issues in the build files.
 1.8.3 - Added support for shared cache mode
         Added support for access to original SQL statement for prepared statements
         (requires SQLite 3.5.3 or above)
         Fixed broken SQLite DLLs
 1.8.2 - Upgrade to SQLite version 3.5.4
         Fixed a bug in in wxSQLite3Database::Begin (wrong transaction type)
 1.8.1 - Fixed a bug in in wxSQLite3Database::Close (resetting flag m_isEncrypted)
         Eliminated several compile time warnings (regarding unused parameters)
         Fixed a compile time bug in wxSQLite3Database::GetBlob (missing explicit type cast)
 1.8.0 - Upgrade to SQLite version 3.5.2
         Support for SQLite incremental BLOBs
         Changed source code in the SQLite3 encryption extension
         to eliminate several warnings
         Changed default wxWidgets version to 2.8.x
         Adjusted sources for SQLite encryption support are included
         for all SQLite version from 3.3.1 up to 3.5.2
         SQLite link libraries for MinGW on Windows are included
         Added <code>WXMAKINGLIB_WXSQLITE3</code> compile time option
         to support building wxSQLite3 as a static library while
         using the shared libraries of wxWidgets.
 1.7.3 - Upgrade to SQLite version 3.3.17
         Fixed a bug in the SQLite3 encryption extension
         (MD5 algorithm was not aware of endianess on
         big-endian platforms, resulting in non-portable
         database files)
 1.7.2 - Upgrade to SQLite version 3.3.11
         Support for loadable extensions is now optional
         Check for optional wxSQLite3 features at runtime
         wxSQLite3 API independent of optional features
 1.7.1 - Fixed a bug in the key based database encryption feature
         (The call to sqlite3_rekey in wxSQLite3Database::ReKey
         could cause a program crash, when used to encrypt a previously
         unencrypted database.)
 1.7.0 - Upgrade to SQLite version 3.3.10
         Added support for BLOBs as wxMemoryBuffer objects
         Added support for loadable extensions
         Optional support for key based database encryption
 1.6.0 - Added support for user-defined collation sequences
 1.5.3 - Upgrade to SQLite version 3.3.6
         Added support for optional SQLite meta data methods
 1.5.2 - Fixed a bug in wxSQLite3Database::Prepare
         Added wxSQLite3Database::IsOpen for convenience
 1.5.1 - SQLite DLL upgraded to version 3.3.4
 1.5   - Upgrade to SQLite version 3.3.3
         Added support for commit, rollback and update callbacks
 1.4.2 - Optimized code for wxString arguments
 1.4.1 - Fixed a bug in TableExists, eliminated some compiler warnings
         Changed handling of Unicode string conversion
         Added support for different transaction types
 1.4   - Optionally load SQLite library dynamically at run time
 1.3.1 - Corrected wxSQLite3ResultSet::GetInt64,
         added wxSQLite3Table::GetInt64
 1.3   - Added wxGTK build support
 1.2   - Corrected error in wxSQLite3Table::FindColumnIndex
 1.1   - Upgrade to SQLite version 3.2.7
 1.0   - First public release


Installation
------------

a) wxMSW

When building on win32, you can use the makefiles in the BUILD folder.

SQLite version 3.6.22 DLL is included. The included link library was
built with MS Visual C++ 6. For other compilers it can be necessary to
regenerate the link library based on the sqlite.def file in the LIB
folder.

mingw: dlltool -d sqlite3.def -D sqlite3.dll -l sqlite3.a

(As demanded by several users sqlite3.a for mingw is now included.)

The directory sqlite3/lib contains the original DLL from the SQLite
distribution.

In prior versions of wxSQLite3 the directory sqlite3/meta contained
a special DLL version including support for the optional SQLite meta
data methods. Since meta data support is now enabled in the SQLite binary
distribution as a default, the DLL has been dropped from the wxSQLite3
distribution.

To get the sample application to work the SQLite DLL to be used has to
be copied to the samples directory.

If you want to use the optional SQLite meta data methods SQLite needs
to be compiled with SQLITE_ENABLE_COLUMN_METADATA (as is the case for
the DLL version in directory sqlite3/meta). Additionally the
preprocessor symbol WXSQLITE3_HAVE_METADATA must be defined when
compiling wxSQLite3.

All included SQLite DLLs have the FTS3 module enabled.

On user request a precompiled SQLite shell program supporting encrypted
databases is included. Use

PRAGMA KEY="encryption key";

to create or open an encrypted database. Use

ATTACH DATABASE x AS y KEY z;

to attach an encryted database.

b) wxGTK

When building on an autoconf-based system (like Linux/GNU-based
systems), you can use the existing configure script in the component's
ROOT folder or you can recreate the configure script doing:

  cd build
  ./acregen.sh
  cd ..

  ./configure [here you should use the same flags you used to configure wxWidgets]
  make
 
Type "./configure --help" for more info.

Note: Recreating the configure script requires the following prerequisites:
- automake 1.9.6 or higher
- bakefile 0.2.5
- wxCode autoconf and bakefile files (to be downloaded from CVS or SVN)

The autoconf-based systems also support a "make install" target which
builds the library and then copies the headers of the component to
/usr/local/include and the lib to /usr/local/lib.

SQLite version 3.6.22 is NOT included. You have to download the current
version of SQLite from http://www.sqlite.org and to install it on your
system before you can install wxSQLite3.

Use the configure option --with-sqlite3-prefix to specify the path to your
SQLite3 installation.


Optional Meta Data support
--------------------------

If you want to use the optional SQLite meta data methods SQLite needs
to be compiled with SQLITE_ENABLE_COLUMN_METADATA. Additionally the
preprocessor symbol WXSQLITE3_HAVE_METADATA must be defined when
compiling wxSQLite3.


Optional key based database encryption support
----------------------------------------------

The public release of SQLite contains hooks for key based database
encryption, but the code for implementing this feature is not freely
available. D. Richard Hipp offers a commercial solution
(see http://www.hwaci.com/sw/sqlite/prosupport.html#crypto).

If you want to use the optional SQLite key based database encryption
you need to have the implementation of this feature and you have to
compile SQLite with the option SQLITE_HAS_CODEC. 

Additionally the preprocessor symbol WXSQLITE3_HAVE_CODEC must be
defined when compiling wxSQLite3.

There exist other commercial solutions, among them:

http://www.sqlcrypt.com
http://www.sqlite-crypt.com

Both use a slightly different encryption API, which is currently NOT
supported by wxSQLite3.

For Windows based systems there exists an open source solution:
System.Data.SQLite (see http://sqlite.phxsoftware.com). Encrypted
database files can be shared across Windows platforms only.

The author of wxSQLite3 has created a key based AES database encryption
implementation for SQLite such that sharing encrypted database files
across different platforms is supported. The implementation is based on
knowledge gained from implementing encryption support for the wxCode
component wxPdfDocument and from inspecting the source code of
System.Data.SQLite. The code implementing this feature is available in
directory sqlite3/secure/src. You may use this code on your own risk.
The subdirectory codec contains the necessary source files; copy the
complete subdirectory to the location where the source code of SQLite
resides on your system.

With the release of wxSQLite3 1.9.6 the encryption extension has been
converted from C++ to pure C and is now compatible with the SQLite
amalgamation source distribution. Just compile the file sqlite3secure.c
which includes all required source files. Note that the SQLite source
itself is not included in the wxSQLite3 file release. Don't forget to
add option SQLITE_HAS_CODEC to enable encryption support.

With the release of wxSQLite3 1.9.8 the encryption extension includes
an experimental implementation of 256 bit AES encryption. The code
comes without any warranty, use it at your own risk.
Currently the decision whether to use 128 bit or 256 bit AES encryption
has to be made at compile time. To enable 256 bit AES encryption define
CODEC_TYPE=CODEC_TYPE_AES256 (Default: CODEC_TYPE=CODEC_TYPE_AES128).

For wxMSW the directory sqlite3/secure contains a special DLL version
including support for the optional SQLite meta data methods and the
optional key based database encryption (128 bit AES).


Using statically linked SQLite library on Windows
-------------------------------------------------

If you want or need to compile wxSQLite3 lib in such a way that it
does not require the sqlite3.dll at run-time, you have to build a
static sqlite3 library. No modifications to wxSQLite3 are required. 

The following steps are required:

- You have to download the complete source code for sqlite from
  http://www.sqlite.org/download.html. Look for the ZIP archive since
  it already includes all generated sources and is ready to compile on
  Windows without the need for additional tools.
- Unfortunately no makefile is included. That is, you have to create a
  makefile or project file yourself. You have to build a static library
  from all C source files except shell.c (which is the stand alone tool
  for sqlite3) and tclsqlite.c (which is the tcl wrapper).
- Replace the file sqlite3.lib in the wxSQLite3 distribution by the
  sqlite3.lib created as stated above and compile wxSQLite3 as a static
  library.
- Don't forget to set USE_DYNAMIC_SQLITE3_LOAD=0. 

Acknowledgements
----------------

The following people have contributed to wxSQLite3:

Francesco Montorsi (enhancement of the build system)
Neville Dastur (enhancement of the method TableExists)
Tobias Langner (RAII class for managing transactions)

Known bugs
----------

None


Authors' info
-------------

Ulrich Telle   utelle@users.sourceforge.net
