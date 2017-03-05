### Box9D LED Pi API ###

Box9D LED Pi API sits on raspbian on the raspberry Pi and should be used in conjunction with the Box9D LED Manager for configuring and playing back LED light shows

### How do I get set up? ###

- Create a folder on the Pi ~/dotnetboot and copy the files from the scripts folder in this repository (getlatest.sh and start.sh)
- Ensure execute permissions are given to both script files (chmod +x)
- Add the following lines to the /etc/rc.local file (using nano or equivalent) before the exit 0 command:
  /home/pi/dotnetboot/getlatest.sh
  /home/pi/dotnetboot/start.sh

Thats it. That should update to the latest build of the Box9D LED Pi API on boot, and then run.

