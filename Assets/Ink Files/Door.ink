VAR contiene_item = 0 

#tienellave
{ contiene_item <= 0: 
-> CERRADA 
-else: 
-> ABIERTA
}

    == CERRADA ==
    - Esta cerrada.
    - Justo sobre el cerrojo esta la inscripción Rayos X 1
    -> END
    
    == ABIERTA ==
    - Usaste la llave de la Sala de Rayos.
    #abrirpuerta
    -> END
    
    
=== function asignar_contiene_item (x)====
~ contiene_item = x