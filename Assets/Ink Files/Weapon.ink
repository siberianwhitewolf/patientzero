VAR contiene_item = 0 

- ....
- !¿De dónde salió esta arma?!...
- Parece que la voy a necesitar...
- ¿Me la llevo?
-> OPCIONES

==OPCIONES==
*[SI] -> SI
*[NO] -> NO

== SI ==
Obtienes una pistola automática 9mm. #alice
    -> END
    
==NO==
-> END

=== function asignar_contiene_item (x)====
~ contiene_item = x