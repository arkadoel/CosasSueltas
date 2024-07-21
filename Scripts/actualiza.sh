#!/bin/bash
RED='\033[0;31m'   #'0;31' is Red's ANSI color code
GREEN='\033[0;32m'   #'0;32' is Green's ANSI color code
YELLOW='\033[1;32m'   #'1;32' is Yellow's ANSI color code
BLUE='\033[0;34m'   #'0;34' is Blue's ANSI color code
NOCOLOR='\033[0m'

echo -e "${RED}"
echo "##############################################"
echo "########   Flatpack updates #################"
echo "##############################################"
echo -e "${NOCOLOR}"
sudo flatpak update -y 
echo -e "${GREEN}"
echo "##############################################"
echo "############    Snap updates #################"
echo "##############################################"
echo -e "${NOCOLOR}"
sudo snap refresh 
echo -e "${YELLOW}"
echo "##############################################"
echo "######    Linux Mint updates #################"
echo "##############################################"
echo -e "${NOCOLOR}"
sudo apt update && sudo apt full-upgrade -y --allow-unauthenticated && sudo apt autoclean -y
echo -e "${BLUE}"
echo "###########    End    #################"
echo -e "${NOCOLOR}"
