INCLUDE globalVariables.ink

    { missione:
       -0: ->intro
       -1 :->mission
       -2 :->addMissione
         -3 : -> completed
       -4 : -> completed
       -5 : ->completed
       -6 : ->completed
       }
       

===intro===
-Benvenuto a Neapolis!

->END

===mission===

-comm si bell!!
~missione = missione + 1
->END
 
===completed===
-sei tal e qual al mio fu marito!
->END
            
===addMissione===
~MissionCompleted = "X"
~missione = missione + 1
-sei tal e qual al mio fu marito!
-vai dal sindaco, ti sta cercando!
->END