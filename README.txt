INFORMATIONS CONCERNANT L'EDITEUR

L'Atomix Editor n'est bien évidement pas sans faille. 

Lisez le tutoriel pour avoir plus d'informations sur l'utilisation de l'éditeur.

Voici donc quelques informations concernant les différentes précotions à mettre en place lors de son utilisation.


- CREATION DE LA MAP

Lors de la création de la map, en théorie il n'est pas possible de créer la map avec un champ vide.
En revanche, les champs ne sont pas vérifiés. 
Si une chaine de caractère est entrée dans les cases où des entiers sont attendu, le logiciel va plenter.

Il en va de même si le chemin est incorect, ou si le fichier selectionné pour le tileset n'est pas une image.

En théorie, tout tileset peut être géré par l'éditeur (La taille des cases ne devrait pas dépasser 99 en hauteur et largeur).


- CHARGEMENT D'UNE MAP

Il suffit de sélectionner un fichier XML crée par notre éditeur.
Attention cependant, rien ne doit avoir été modifié dans l'ordre des attributs des balises!
Si une seule modification a été effectuée dans la structure du XML, l'éditeur risque fortement de plenter.

Un gros problème de cet éditeur: les fichiers XML ne peuvent par réellement être partagés.
En effet, nous gèrons le chemin du tileset en fonction de l'ordinateur qui l'a crée.
Il aurai fallu que nous chargions le tileset aussi pour resauvegarder sa postition.
Le mieux aurai été de créer une position relative à l'executable.


- EDITION

Normalement, l'éditeur en lui même n'est pas bogué. Toutes les fonctionnalitées devraient être oppérationnels.
Il n'y a pas encore de ctrl+Z.
Si vous cliquez sur RAZ, aucun message d'alerte n'apparaîtra (c'est instantané, attention).
Vous ne pouvez pas encore changer le nom du fichier avec un save as.
Vous ne pouvez dessiner que sur un seul layer.
Vous ne pouvez utiliser qu'une seule map par tilemap.
Les images du tileset se réajuste à la taille des cases de la tilemap.