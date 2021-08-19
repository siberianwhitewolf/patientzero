VAR contiene_item = 0

- Abres el cajon

{ contiene_item > 0: 
-> HAY_LLAVE
-else: 
-> NO_HAY_LLAVE
}


==HAY_LLAVE==
- Entre varios documentos viejos encuentras un objeto resplandeciente
- Es una llave.
- ¿La tomas?

* [TOMAR] -> SI
* [DEJAR] -> NO

 == SI ==
    Obtienes la llave de la sala de rayos. #llave
    ~ contiene_item = 0 
    -> END
    
    == NO ==
    Cierras el cajón.
    -> END

->END

==NO_HAY_LLAVE==
- No hay nada de interes.
-> END

=== function asignar_contiene_item (x)====
~ contiene_item = x