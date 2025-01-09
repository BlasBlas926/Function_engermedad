-- FUNCTION: admece.fn_get_all_page_fetch_cronica(integer, integer)

-- DROP FUNCTION IF EXISTS admece.fn_get_all_page_fetch_cronica(integer, integer);

CREATE OR REPLACE FUNCTION admece.fn_get_all_page_fetch_cronica(
	p_page integer,
	p_fetch integer)
    RETURNS TABLE(id_enf_cronica integer, nombre character varying, descripcion character varying, fecha_registro date, fecha_inicio date, estado boolean, fecha_actualizacion date) 
    LANGUAGE 'plpgsql'
    COST 100
    STABLE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    RETURN QUERY
    SELECT
        ec.id_enf_cronica,
        ec.nombre,
        ec.descripcion,
        ec.fecha_registro,
        ec.fecha_inicio,
        ec.estado,
        ec.fecha_actualizacion
    FROM
        admece.tc_enfermedad_cronica ec
		    WHERE
        ec.estado = true
    ORDER BY
        ec.id_enf_cronica ASC
    LIMIT p_fetch
    OFFSET (p_page - 1) * p_fetch;

if p_page <= 0 or p_fetch <=0 then
return query select * from admece.obtener_todos_enfermedades();
end if;
	
END;
$BODY$;

ALTER FUNCTION admece.fn_get_all_page_fetch_cronica(integer, integer)
    OWNER TO postgres;
