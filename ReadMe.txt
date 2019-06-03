
   ____ ____  _____ _                
  / ___/ ___||_   _(_)_ __ ___   ___ 
 | |  | |      | | | | '_ ` _ \ / _ \
 | |__| |___   | | | | | | | | |  __/
  \____\____|  |_| |_|_| |_| |_|\___|
                                      
   Copyright (c) RL Vision 2018-2019
       https://www.rlvision.com
      Free, Open Source Software


--- About ---

CCTime is a utility to help track how much time you spend on different tasks during your work day. Tracking 
time is tedious, so focus lies on simplicity and automation. CCTime stays out of your way while still being 
readily accessible, so you can focus on actual work. CC is short for Click Click, because two clicks is all 
it takes to register time on a task!

CCTime sits in your system tray and automatically hides when not in use. You can see how much time you've 
spent in total each day, and how much of that time has not yet been assigned to tasks. For each week you can 
get a report, summarizing your work that week.


--- Installation ---

Simply unzip and run. The program is portable and will only write to its own folder.

For convenience you'll probably want to put a shotycut to CCTime.exe in your startup folder. (Tip: to find 
this folder press Win+R and type "shell:startup")


--- Usage ---

CCTime sits quietly in the system tray. Click the icon to show the main window:

+-------------------------------------------------+
| CCTime  [08:00]                                 |		<- Title shows total time today
+-------------------------------------------------+
|  (<)             Monday June 1              (>) |		<- The date you are viewing (typically today)
|                                                 |
|  Total            08:00    [<15]         [15>]  |		<- Total time this date
|  Unaccounted      04:28                         |		<- Difference between total time and time on tasks
|                                                 |
|  Task 1           02:00    [+15]  [+30]  [+60]  |		<- A task named "Task 1" with 2 hours work
|  Task 2*          01:00    [+15]  [+30]  [+60]  |		<- A task with a comment (hidden, indicated by *)
|  Task 3         o 00:32    [+15]  [+30]  [+60]  |		<- A task that ticks along with time (indicated by o)
|                                                 |
|  (+) (-) (U) (D) (#)                  (@)  (/)  |		<- Buttons for managing tasks, days, reports etc
+-------------------------------------------------+

At a glance:
  - Total time automatically ticks. It shows how much time you've been working each day
  - Click the (+) button to add a task
  - As you work throughout the day, click the buttons next to tasks to distribute time
  - Unaccounted time = Total time - Time allocated to tasks. (0 means all time is accounted for!)
  - Click (#) to show a weekly summary report
  - Clicking outside the window automatically hides CCTime. Use the pin (/) to stay on top

That's the basics. Start using CCTime or read on to learn the details!

CCTime shows one day at a time
  - Typically this is today's date, but you can view and modify old dates too
  - Change day using the arrow buttons (<) and (>)
  - Click the date label ("Monday June 1" above) for a calendar view
  - Each new day you start CCTime, this date is automatically added and previous day's tasks are copied
  - Note that only days when CCTime was started or dates that have been explicitly added are available

The window title shows the total time spent *today*
  - A background timer automatically keeps track of how much time has been spent today (even when viewing other 
    days!).
  - Toggle the timer on/off using the clock button (@)

"Total" row shows the total time spent the day you are viewing
  - Use the (<15) and (15>) buttons to shift total time to nearest 15 min
  - Hold Click to instead add and subtract 15 minutes

"Unaccounted" row shows the difference between total time and time spent on all task
  - 0 means all time is accounted for, which is typically what to aim for at the end of the day

Next comes all the tasks.
  - Double click to edit the task title.
  - Use a semicolon to add a comment to a task ("task title;the comment"). Comments are hidden but indicated 
    by an * (see Task 2 above). The report will show comments.
  - For each task you see how many hours you've spent on that task this day
  - Use the buttons to add time to the task. Hold Shift to instead subtract time, or Ctrl to shift 
    time to nearest 15 minutes.
  - Right click a task to set it to automatically increase with time. This is indicated with a bullet in 
    front of the name (see Task 3 above).

The button on the lower left hand side manages tasks and days
 - Add and remove *tasks* using (+) and (-)
 - Ctrl-Click (+) to add a new day or (-) to remove current *day*
 - Alt-Click (-) to remove the current day's tasks that have 0 time
 - (U) and (D) moves tasks up and down a step (or to top/bottom if holding Ctrl)

Click the calendar icon (#) to generate a report of the current week.
  - The report shows all tasks and the amount spent per day, plus summary columns and rows
  - The report includes comments. Two tasks with the same name but different comments are treated as 
    different tasks
  - Current week means the week of the date currently displayed

Tips & Tricks
  - At the end of the day, stop the timer and shift the total time to an even number, then fill up task time 
    to "zero out" unaccounted time
  - You can double click time fields to manually edit the number of minutes
  - Right click the system tray icon for a menu with help and settings
  - The settings file allows you to tweak the program in many ways to suit your style and language, 
    especially the report
  - Use left/right on the keyboard to move between days

Notes
  - The internal clock stops whenever a modal dialog is opened, e.g. when adding a task (but not the report)


--- Settings and Data ---

There is a file called settings.ini that you can edit to tweak the program to your preferences. The program 
must be restarted for settings to apply.

Task data is stored in the "data" folder. Each day has its own file. Data is stored in a CSV style format that 
should be self explanatory. If needed you can easily edit these files by hand, or add and remove data 
files. Just make sure you close the program first.


--- Privacy Notice ---

CCTime does not connect to the internet. It does not phone home, check for updates, submit telemetry, spy on 
you or any other crap like that. It simply doesn't do anything behind your back and any data related to the 
program is yours. As it should be.


--- Credits ---

"small-n-flat" icon set by paomedia
License: CC0 1.0 Universal
Website: https://github.com/paomedia/small-n-flat

"ObjectListView" ©2006-2016 Phillip Piper
License: GPLv3
Website: http://objectlistview.sourceforge.net/cs/index.html

ini-parser (c) 2008 Ricardo Amores Hernández
License: The MIT License (MIT)
Website: https://github.com/rickyah/ini-parser


--- History ---

v1.00	2019-06-03	First public release
v1 RC	2019-02-08	Distributed internally at work
v0.00	2018-04-xx	Developed for personal usage
