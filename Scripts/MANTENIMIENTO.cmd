echo off
cls
TITLE OPTIMIZANDO E:
CHKDSK E: 
defrag E: /U /V /H /X

TITLE OPTIMIZANDO D:
CHKDSK D: 
defrag D: /U /V /H /X

TITLE OPTIMIZANDO F:
CHKDSK F: 
defrag F: /U /V /H /X

PAUSE