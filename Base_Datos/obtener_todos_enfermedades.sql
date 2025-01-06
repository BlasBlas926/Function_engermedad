-- FUNCTION: admece.obtener_todos_enfermedades()

-- DROP FUNCTION IF EXISTS admece.obtener_todos_enfermedades();

CREATE OR REPLACE FUNCTION admece.obtener_todos_enfermedades(
	)
    RETURNS TABLE(id_enf_cronica integer, nombre character varying, descripcion character varying, fecha_registro date, fecha_inicio date, estado boolean, fecha_actualizacion date) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    RETURN QUERY 
    SELECT t.id_enf_cronica, t.nombre, t.descripcion, t.fecha_registro, t.fecha_inicio, t.estado, t.fecha_actualizacion
    FROM admece."tc_enfermedad_cronica" t
    WHERE t.estado = TRUE
    ORDER BY t.id_enf_cronica ASC;
END;
$BODY$;

ALTER FUNCTION admece.obtener_todos_enfermedades()
    OWNER TO postgres;
