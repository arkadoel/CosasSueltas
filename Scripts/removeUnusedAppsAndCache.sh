#!/bin/bash
RED='\033[0;31m'   #'0;31' is Red's ANSI color code
GREEN='\033[0;32m'   #'0;32' is Green's ANSI color code
YELLOW='\033[1;32m'   #'1;32' is Yellow's ANSI color code
BLUE='\033[0;34m'   #'0;34' is Blue's ANSI color code
NOCOLOR='\033[0m'

echo -e "${GREEN}"
echo "##############################################"
echo "########   Flatpack unUsed #################"
echo "##############################################"
echo -e "${NOCOLOR}"
sudo flatpak uninstall --unused && sudo flatpak repair
echo -e "${GREEN}"
echo "##############################################"
echo "############    Snap unUsed #################"
echo "##############################################"
echo -e "${NOCOLOR}"
LANG=C snap list --all | while read snapname ver rev trk pub notes; do if [[ $notes = disabled || $notes = desactivado ]]; then sudo snap remove "$snapname" --revision="$rev"; fi; done
echo -e "${YELLOW}"
echo "##############################################"
echo "######    Linux Mint clean #################"
echo "##############################################"
echo -e "${NOCOLOR}"
sudo sudo apt autoclean -y && sudo apt-get clean -y
echo -e "${BLUE}"
echo "###########    End    #################"
echo -e "${NOCOLOR}"
