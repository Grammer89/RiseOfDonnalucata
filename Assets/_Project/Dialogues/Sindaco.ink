INCLUDE GlobalVariables.ink

 { missione:
       -0: ->intro
       -1 :->intro
       -2 :->intro 
       -3 : -> quest
       -4 : -> waitingfor
       -5 : ->completed
       }
       
       
       
===intro===
-Benvenuto a Neapoli, sono il sindaco!
->END
===quest===
~MissionCompleted = ""
-Valoroso guerriero, potresti darci una mano?
 + [Si] ->Si
 +[No] ->No
 
===Si===
-Sconfiggi i nemici alle porte della città
~missione = missione + 1
~MissionId = "Paladin"
->END
===No===
-Oh,capisco!
->END
===waitingfor===
-I nemici sono alle porte della città
->END
===completed===
-Grazie Paladino, questo è solo un piccolo passo nella tua avventura!
 ~ComingSoon = "X"
 ->END