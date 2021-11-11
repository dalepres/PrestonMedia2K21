Preston Media Metadata Backup

License:

Dale Preston, http://www.dalepreston.com, grants the user the right to use Preston Media Metadata Backup on as many computers as he or she likes for the purpose of copying and viewing the metadata for their music files.  To use Preston Media Metadata Backup, you must agree that they are using it solely at their own risk and will not hold Dale Preston or his heirs or assigns responsible for any damage or loss of data resulting from its use.  You must also agree that Dale Preston is not responsible for providing any support for Preston Media Metadata Backup.

While I believe Preston Media Metadata BAckup to be safe to use, there are many things that can cause the loss of data including user error or unexpected behavior from badly malformed data.  You should only use Preston Media Metadata backup on files that have been properly backed up.  That just makes good sense - always backup files before modifying them in any way.

You may redistribute Preston Media Metadata Backup only as a combination of this unedited MetadataBackupReadMe.txt file and the original unmodified executable file.

Revision History:

5/28/2007 - Version 1.1

UTF-8 XML data does not like null characters (bytes containing the value of zero).  Some ID3 tag editing programs insert a null character to divide lists such as composers or genres.  Version 1.1 of MetadataBackup fixes an issue that occurred when encountering those null characters by replacing the null character with the string "<NULL>" when backing up and replacing the string "<NULL>" with a null character when restoring.

Also, there was added some detailed logging, turned off by default, that can be used to troubleshoot further data compatability issues.

3/18/2007 - Initial release.

For more help using Metadata Backup, read the MetadataBackupHelp.rtf file included in the download or available from http://www.dalepreston.com/downloads/MetadataBackupHelp.rtf.
