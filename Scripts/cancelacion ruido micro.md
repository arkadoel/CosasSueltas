sudo xed /etc/pulse/default.pa

sobre la linea 140 añadir 
load-module module-echo-cancel

reiniciar con pulseaudio -k
