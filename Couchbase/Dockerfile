FROM couchbase:community

COPY configure-node.sh /opt/couchbase

#HEALTHCHECK --interval=5s --timeout=3s CMD curl --fail http://localhost:8091/pools || exit 1
EXPOSE 8091
EXPOSE 11210

CMD ["/opt/couchbase/configure-node.sh"]