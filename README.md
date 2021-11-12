# PrestonMedia2K21

This solution is intended to combine all of my media related apps into a single Visual Studio and github solution
just to prevent having copies scattered around my PC.  Many of these were written 15 or more years so I may not
remember accurately what's in them or all that they do.  I use them still for their basic functionality but you, 
and I, may need to dig further to fully appreciate or understand them.

There are more apps to add, to test, and to refactor/improve but, for now, here are the apps included:

CommonControls - This is a library for user controls used in multiple apps.  The only control in it now is a folder 
selector based on the standard WinForms folder selector.  It adds some functionality like a drop down for suggesting 
recent selections or options to include subfolders, browse, etc.  It also has events for folder selection, etc.

DPlayer - this provides the now-playing interface that Windows Media Player, at least based on my preferneces, 
should have had.  Where Microsoft and Windows Media Player try to limit you to displayed album art of, literally, 
less than 200x200 pixels, DPlayer will display album art up to nearly the full height of your screen.  For instance, 
on my full HD (1920 x 1080), I use 1000x1000 album art.  On my 4K monitor, I use 2000x2000 pixel album art.  
Actually, with player tools and basic track info displayed on the screen, those sizes are a bit too large - the 
perfect condition for stored album art with DPlayer.  DPLayer will scale down to fit available space on the app 
window but will only scale up to the maximum original size of the album art - compared to Windows Media Player that 
will scale down your art to 180x180 pixels then scale up (which always distorts) to a paltry 200x200 pixels.  The UI 
and features in DPlayer are basic but you should check it out.  For what it does, I like it - which is why I wrote it.

ID3AlbumArtFixer - Because Microsoft uses Windows Media Player as a back door into your computer and, maliciously in 
my opinion, deletes your data without your permission, such as if you make custom album art for your media, Microsoft 
delete it without asking, without permission, and without notification, and replace it with crappy, in comparison, 
200x200 pixel album art, I wrote ID3AlbumArtFixer to protect and manage my album art.  To take the best advantage of 
ID3AlbumArtFixer, get the highest resolution and quality album art you can legally and ethically obtain.  I get mine 
by using my scanner to take a digital image of my album covers at high resolution so I end up with an image usually 
around 6000x6000 pixels.  This allows me to scale down with far less loss.  ID3AlbumArtFixer will then make a copy 
of that image scaled down to what size you set, I usually use between 1000x1000 pixels and 2000x2000 pixels and save 
that as Folder.jpg in the album folder where most media players, including DPlayer, will use it to display while 
playing.  Additioanlly, ID3AlbumArtFixer will apply Windows file security, known as Access Control Lists, or ACLs, 
to the Folder.jpg file so neither Windows nor Media Player, so far even after 10 years of use, has not yet been able 
to delete my custom album art.  Lastly, ID3AlbumArtFixer will embed your album art, again at whatever size you 
specify, to the individual MP3 files.  If you're struggling with managing your existing album art, or tired of Windows 
and Media Player deleting your intellectual property to replace it with their, in my opinion, garbage, check out 
ID3AlbumArtFixer.
