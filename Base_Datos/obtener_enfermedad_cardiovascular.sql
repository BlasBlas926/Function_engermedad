-- FUNCTION: admece.obtener_enfermedad_cardiovascular()

-- DROP FUNCTION IF EXISTS admece.obtener_enfermedad_cardiovascular();

CREATE OR REPLACE FUNCTION admece.obtener_enfermedad_cardiovascular(
	)
    RETURNS TABLE(id_enf_cardiovascular integer, nombre character varying, descripcion character varying, fecha_registro date, fecha_inicio date, estado boolean, fecha_actualizacion date) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    RETURN QUERY 
    SELECT t.id_enf_cardiovascular, t.nombre, t.descripcion, t.fecha_registro, t.fecha_inicio, t.estado, t.fecha_actualizacion
    FROM admece."tc_enfermedad_cardiovascular" t
    WHERE t.estado = TRUE
    ORDER BY t.id_enf_cardiovascular ASC;
END;
$BODY$;

ALTER FUNCTION admece.obtener_enfermedad_cardiovascular()
    OWNER TO postgres;
