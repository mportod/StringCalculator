#### 1. Descripción de la Kata

- Consiste en una aplicación que pueda a partir de una cadena de texto con números separados por comas, sumar dichos números y devolver el resultado.
- Es una práctica que se suele desarrollar por partes y progresiva.

`Paso 1`
◾Antes de empezar:
- Intenta no leer más allá del requisito que estás implementando en cada momento.
- Realiza una tarea a la vez. EI truco está en aprender a trabajar incrementalmente.
- Asegúrate de probar únicamente las entradas correctas. No hay necesidad de probar entradas inválidas en este ejercicio.

◾Crea una calculadora de strings simple con un método con la siguiente firma `int Add(string numbers)`
- El método recibe hasta dos números separados por comas, devolviendo su suma 
- por ejemplo "" o "1" o "1,2" como entrada (para una cadena vacía, devolverá O).

	**Pistas:**
		• Comienza con el test más simple para una cadena vacía, sigue probando el caso de un número y luego el caso de dos números.
		• Recuerda resolver las tareas de la forma más simple posible, para forzarte a escribir casos de tests que quizá no se te ocurrirían de otra forma.
		• Recuerda refactorizar tras lograr llegar a verde.

`Paso 2`
Añadir la posibilidad de que el método Add reciba una cantidad indeterminada de números

`Paso 3`
Añadir la posibilidad de que el método Add soporte retornos de carro como separador (además de comas)

`Paso 4`
- Soportar diferentes separadores
- El comienzo de la cadena debe contener una linea separada con este formato `//[delimiter]\n[numbers]`
- Todos los escenarios implementados hasta el momento deben seguir funcionando.

`Paso 5`
- No permitir números negativos
- Llamar al método Add con un número negativo deberá lanzar una excepción "negatives not allowed" - y el número negativo pasado como argumento.
- Si hay múltiples negativos, muéstralos todos en el mensaje de la excepción.

`PASO 6`
- Los números mayores a 1000 deberían ignorarse
- Es decir, sumar 2+ 1001 = 2

`PASO 7`
- Los separadores ahora pueden ser de cualquier longitud si se especifican con el siguiente formato: `//[delimiter]\n[numbers]`
- ejemplo: `//[***]\n1***2***3` debe devolver **6**

`PASO 8`
- Soportar múltiples separadores con este formato: `//[delim1][delim2]\n[numbers]"`
- ejemplo: `//[*][%]\n1*2%3` debe devolver **6**

`PASO 9`
- Asegúrate de que se soportan múltiples separadores cuando la longitud es mayor a un carácter.


#### 2. Resolucion

En este caso vamos paso a paso desarrollando de forma incremental usando cada paso como la nueva "partición" de comportamiento a tratar
