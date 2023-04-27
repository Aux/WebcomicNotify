# WebcomicNotify
A cli app for polling and notifying of when new webcomic chapters are released.

### Usage  
`webcomicnotify [command] [options] [[--] <additional arguments>...]]`

#### Setup
Add a comic with  
`webcomicnotify add --url https://www.webtoons.com/en/action/omniscient-reader/rss?title_no=2154`  
Or  
`webcomicnotify add webtoon omniscient reader`  

Run the app with the required arguments  
`webcomicnotify --webhook https://discord.com/api/webhooks/{guild_id}/{webhook_id}`  

Then sit back and relax!

### Arguments
`-w, --webhook`  
Required: The default webhook to post notifications to.  
`-p, --pollrate`  
Optional: How often in minutes the service will poll for new chapters. [default: 60]  

### Commands  
`list, ls`  
Show a list of all configured webcomics.  
`check, force`  
Force a check to run without the polling service.  
`latest`  
Post the latest chapter from a feed, regardless of whether it's already been posted.  
`del, delete, remove`  
Remove a webcomic from the polling service.  
`add, new`  
Add a webcomic to the polling service.  